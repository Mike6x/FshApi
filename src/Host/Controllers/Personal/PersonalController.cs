using FSH.WebApi.Application.Auditing;
using FSH.WebApi.Application.Identity.Tokens;
using FSH.WebApi.Application.Identity.Users;
using FSH.WebApi.Application.Identity.Users.Password;
using System.Security.Claims;

namespace FSH.WebApi.Host.Controllers.Identity;

public class PersonalController : VersionNeutralApiController
{
    private readonly IUserService _userService;
    private readonly ITokenService _tokenService;

    public PersonalController(IUserService userService, ITokenService tokenService) =>
        (_userService, _tokenService) = (userService, tokenService);

    [HttpGet("profile")]
    [OpenApiOperation("Get profile details of currently logged in user.", "")]
    public async Task<ActionResult<UserDetailsDto>> GetProfileAsync(CancellationToken cancellationToken)
    {
        return User.GetUserId() is not { } userId || string.IsNullOrEmpty(userId)
            ? Unauthorized()
            : Ok(await _userService.GetAsync(userId, cancellationToken));
    }

    [HttpPut("profile")]
    [OpenApiOperation("Update profile details of currently logged in user.", "")]
    public async Task<ActionResult> UpdateProfileAsync(UpdateUserRequest request)
    {
        if (User.GetUserId() is not { } userId || string.IsNullOrEmpty(userId))
        {
            return Unauthorized();
        }

        await _userService.UpdateAsync(request, userId);
        return Ok();
    }

    [HttpPut("change-password")]
    [OpenApiOperation("Change password of currently logged in user.", "")]

    [ApiConventionMethod(typeof(FSHApiConventions), nameof(FSHApiConventions.Register))]
    public async Task<ActionResult> ChangePasswordAsync(ChangePasswordRequest model)
    {
        if (User.GetUserId() is not { } userId || string.IsNullOrEmpty(userId))
        {
            return Unauthorized();
        }

        await _userService.ChangePasswordAsync(model, userId);
        return Ok(true);
    }

    [HttpGet("permissions")]
    [OpenApiOperation("Get permissions of currently logged in user.", "")]
    public async Task<ActionResult<List<string>>> GetPermissionsAsync(CancellationToken cancellationToken)
    {
        return User.GetUserId() is not { } userId || string.IsNullOrEmpty(userId)
            ? Unauthorized()
            : Ok(await _userService.GetPermissionsAsync(userId, cancellationToken));
    }

    [HttpGet("logs")]
    [OpenApiOperation("Get audit logs of currently logged in user.", "")]
    public Task<List<AuditDto>> GetLogsAsync()
    {
        return Mediator.Send(new GetMyAuditLogsRequest());
    }

    #region My Customize
    [HttpGet("logout")]
    [OpenApiOperation("Logout of currently logged in user.", "")]
    public async Task<ActionResult> LogoffAsync(CancellationToken cancellationToken)
    {
        if (User.GetUserId() is not { } userId || string.IsNullOrEmpty(userId))
        {
            return Unauthorized();
        }

        return Ok(await _tokenService.RevokeTokenAsync(userId, cancellationToken));
    }

    [HttpPost("logout")]
    [OpenApiOperation("Logout of currently logged in user.", "")]
    public async Task<ActionResult> LogoutAsync(CancellationToken cancellationToken)
    {
        return User.GetUserId() is not { } userId || string.IsNullOrEmpty(userId)
            ? Unauthorized()
            : Ok(await _tokenService.RevokeTokenAsync(userId, cancellationToken));
    }

    [HttpDelete]
    [OpenApiOperation("Delete a Profile.", "")]
    public ActionResult Delete(CancellationToken cancellationToken)
    {
        return User.GetUserId() is not { } userId || string.IsNullOrEmpty(userId)
            ? Unauthorized()
            : Ok(_userService.DeleteAsync(userId, cancellationToken));
    }

    #endregion
}