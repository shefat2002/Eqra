using System.Security.Claims;

namespace Eqra.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateAccessToken(IEnumerable<Claim> claims);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFormExpiredToken(string token);
    }
}