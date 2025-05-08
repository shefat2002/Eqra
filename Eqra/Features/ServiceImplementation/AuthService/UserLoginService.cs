using System.Security.Cryptography;
using System.Text;
using Eqra.Features.DataAccess.RepositoryInterface;
using Eqra.Features.DomainModels;
using Eqra.Features.ServiceImplementation.ServiceInterface;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace Eqra.Features.ServiceImplementation.AuthService
{
    public class UserLoginService : IUserLoginService
    {
        public readonly IUserRepository _userRepository;
        public readonly ITokenService _tokenService;

        public UserLoginService(IUserRepository userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }
        public async Task<string> LoginUserAsync(LoginDto request)
        {
            var jwt = string.Empty;

            var user = await _userRepository.GetByUsernameAsync(request.Username);

            

            if (user != null)
            {
                var hmac = new HMACSHA512(user.PasswordSalt);
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != user.PasswordHash[i])
                    {
                        jwt = "Invalid password.";
                        return jwt;
                    }
                }

                jwt = await _tokenService.GenerateAccessToken(user);
            }

            else
            {
                jwt = "You are not registered to login.";
            }

            return jwt;
        }
    }
}
