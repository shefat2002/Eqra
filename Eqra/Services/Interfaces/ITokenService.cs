using System.Security.Claims;
using Eqra.Models;

namespace Eqra.Services.Interfaces
{
    public interface ITokenService
    {
        Task<string> GenerateAccessToken(User user);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFormExpiredToken(string token);
    }
}