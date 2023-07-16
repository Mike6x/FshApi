using FSH.WebApi.Application.Common.Exceptions;
using FSH.WebApi.Application.Identity.Tokens;
using FSH.WebApi.Infrastructure.Auth;
using FSH.WebApi.Infrastructure.Auth.Jwt;
using FSH.WebApi.Infrastructure.Multitenancy;
using FSH.WebApi.Shared.Authorization;
using FSH.WebApi.Shared.Multitenancy;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Mail;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace FSH.WebApi.Infrastructure.Identity;

internal class TokenService : ITokenService
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IStringLocalizer _t;
    private readonly SecuritySettings _securitySettings;
    private readonly JwtSettings _jwtSettings;
    private readonly FSHTenantInfo? _currentTenant;

    public TokenService(
        SignInManager<ApplicationUser> signInManager,
        UserManager<ApplicationUser> userManager,
        IOptions<JwtSettings> jwtSettings,
        IStringLocalizer<TokenService> localizer,
        FSHTenantInfo? currentTenant,
        IOptions<SecuritySettings> securitySettings)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _t = localizer;
        _jwtSettings = jwtSettings.Value;
        _currentTenant = currentTenant;
        _securitySettings = securitySettings.Value;
    }

    public async Task<TokenResponse> GetTokenAsync(TokenRequest request, string ipAddress, CancellationToken cancellationToken)
    {
        // if (string.IsNullOrWhiteSpace(_currentTenant?.Id)
        //    || user == null
        //    || !await _userManager.CheckPasswordAsync(user, request.Password))
        // {
        //    throw new UnauthorizedException(_t["Authentication Failed."]);
        // }

        // if (string.IsNullOrWhiteSpace(_currentTenant?.Id)
        //    || await _userManager.FindByEmailAsync(request.Email.Trim().Normalize()) is not { } user
        //    || !await _userManager.CheckPasswordAsync(user, request.Password))
        // {
        //    throw new UnauthorizedException(_t["Authentication Failed."]);
        // }

        #region My customize

        if (_currentTenant == null || string.IsNullOrWhiteSpace(_currentTenant?.Id))
        {
            throw new UnauthorizedException(_t["Tenant Not Found."]);
        }

        if (_currentTenant?.Id != MultitenancyConstants.Root.Id)
        {
            if (!_currentTenant.IsActive)
            {
                throw new UnauthorizedException(_t["Tenant is not Active. Please contact the Application Administrator."]);
            }

            if (DateTime.UtcNow > _currentTenant.ValidUpto)
            {
                throw new UnauthorizedException(_t["Tenant Validity Has Expired. Please contact the Application Administrator."]);
            }
        }

        ApplicationUser? user;
        if (IsValidEmail(request.Email))
        {
            user = await _userManager.FindByEmailAsync(request.Email.Trim().Normalize());
        }
        else
        {
            user = await _userManager.FindByNameAsync(request.Email);
        }

        if (user == null)
        {
            throw new UnauthorizedException(_t["Invalid Email or User Name."]);
        }

        if (!user.IsActive)
        {
            throw new UnauthorizedException(_t["User Not Active. Please contact the administrator."]);
        }

        if (_securitySettings.RequireConfirmedAccount && !user.EmailConfirmed)
        {
            throw new UnauthorizedException(_t["E-Mail not yet confirmed."]);
        }

        var signInResult = await _signInManager.PasswordSignInAsync(user.UserName!, request.Password, true, true);

        if (signInResult.IsLockedOut)
        {
            var logoutEndDate = await _userManager.GetLockoutEndDateAsync(user);
            throw new UnauthorizedException(string.Format(_t["Your account is locked out until {0}."], logoutEndDate.Value.LocalDateTime.ToString()));
        }
        else
        if (!signInResult.Succeeded)
        {
            throw new UnauthorizedException(_t["Wrong Password."]);
        }

        #endregion

        // if (_currentTenant.Id != MultitenancyConstants.Root.Id)
        // {
        //    if (!_currentTenant.IsActive)
        //    {
        //        throw new UnauthorizedException(_t["Tenant is not Active. Please contact the Application Administrator."]);
        //    }
        //    if (DateTime.UtcNow > _currentTenant.ValidUpto)
        //    {
        //        throw new UnauthorizedException(_t["Tenant Validity Has Expired. Please contact the Application Administrator."]);
        //    }
        // }

        return await GenerateTokensAndUpdateUser(user, ipAddress);
    }

    public async Task<TokenResponse> RefreshTokenAsync(RefreshTokenRequest request, string ipAddress)
    {
        var userPrincipal = GetPrincipalFromExpiredToken(request.Token);
        string? userEmail = userPrincipal.GetEmail();
        var user = await _userManager.FindByEmailAsync(userEmail!) ?? throw new UnauthorizedException(_t["Authentication Failed."]);

        if (user.RefreshToken != request.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
        {
            throw new UnauthorizedException(_t["Invalid Refresh Token."]);
        }

        return await GenerateTokensAndUpdateUser(user, ipAddress);
    }

    private async Task<TokenResponse> GenerateTokensAndUpdateUser(ApplicationUser user, string ipAddress)
    {
        string token = GenerateJwt(user, ipAddress);
        user.RefreshToken = GenerateRefreshToken();
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationInDays);
        user.IsLive = true;

        await _userManager.UpdateAsync(user);

        return new TokenResponse(token, user.RefreshToken, user.RefreshTokenExpiryTime);
    }

    private string GenerateJwt(ApplicationUser user, string ipAddress) =>
        GenerateEncryptedToken(GetSigningCredentials(), GetClaims(user, ipAddress));

    private IEnumerable<Claim> GetClaims(ApplicationUser user, string ipAddress) =>
        new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id),
            new(ClaimTypes.Email, user.Email!),
            new(FSHClaims.Fullname, $"{user.FirstName} {user.LastName}"),
            new(ClaimTypes.Name, user.FirstName ?? string.Empty),
            new(ClaimTypes.Surname, user.LastName ?? string.Empty),
            new(FSHClaims.IpAddress, ipAddress),
            new(FSHClaims.Tenant, _currentTenant!.Id),
            new(FSHClaims.ImageUrl, user.ImageUrl ?? string.Empty),
            new(ClaimTypes.MobilePhone, user.PhoneNumber ?? string.Empty)
        };

    private static string GenerateRefreshToken()
    {
        byte[] randomNumber = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    private string GenerateEncryptedToken(SigningCredentials signingCredentials, IEnumerable<Claim> claims)
    {
        var token = new JwtSecurityToken(
           claims: claims,
           expires: DateTime.UtcNow.AddMinutes(_jwtSettings.TokenExpirationInMinutes),
           signingCredentials: signingCredentials);
        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(token);
    }

    private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key)),
            ValidateIssuer = false,
            ValidateAudience = false,
            RoleClaimType = ClaimTypes.Role,
            ClockSkew = TimeSpan.Zero,
            ValidateLifetime = false
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);
        if (securityToken is not JwtSecurityToken jwtSecurityToken ||
            !jwtSecurityToken.Header.Alg.Equals(
                SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCultureIgnoreCase))
        {
            throw new UnauthorizedException(_t["Invalid Token."]);
        }

        return principal;
    }

    private SigningCredentials GetSigningCredentials()
    {
        byte[] secret = Encoding.UTF8.GetBytes(_jwtSettings.Key);
        return new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256);
    }

    private static bool IsValidEmail(string emailaddress)
    {
        try
        {
            MailAddress m = new(emailaddress);
            return true;
        }
        catch (FormatException)
        {
            return false;
        }
    }

    #region My Customize
    public async Task<bool> RevokeTokenAsync(string userId, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(userId) ?? throw new NotFoundException("Application user not found.");

        // user.RefreshTokenExpiryTime = DateTime.UtcNow;
        user.IsLive = false;

        var result = await _userManager.UpdateAsync(user);

        return result.Succeeded;

        // await _signInManager.SignOutAsync();
    }
    #endregion
}