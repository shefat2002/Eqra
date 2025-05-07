using System.Security.Claims;
using Eqra.Features.DomainModels;

namespace Eqra.Features.ServiceImplementation.ServiceInterface
{
    public interface ITokenService
    {
        Task<string> GenerateAccessToken(User user);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFormExpiredToken(string token);
    }
}