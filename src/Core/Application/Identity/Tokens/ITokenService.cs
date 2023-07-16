namespace FSH.WebApi.Application.Identity.Tokens;

public interface ITokenService : ITransientService
{
    Task<TokenResponse> GetTokenAsync(TokenRequest request, string ipAddress, CancellationToken cancellationToken);

    Task<TokenResponse> RefreshTokenAsync(RefreshTokenRequest request, string ipAddress);

    // My customize
    Task<bool> RevokeTokenAsync(string userId, CancellationToken cancellationToken);
}