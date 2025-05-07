using System.Security.Cryptography;
using System.Text;
using Eqra.Features.DataAccess.RepositoryInterface;
using Eqra.Features.DomainModels;
using Eqra.Features.ServiceImplementation.ServiceInterface;
using Newtonsoft.Json;

namespace Eqra.Features.ServiceImplementation
{
    public class AuthService : IAuthService
    {
        public readonly IUserRepository _userRepository;
        public readonly ITokenService _tokenService;

        public AuthService(IUserRepository userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }
        public async Task<object> CreateUserAsync(RegisterDto request)
        {
            var users = await _userRepository.FindAllAsync();
            var uniqueUserNames = new HashSet<string>();
            foreach (var user in users)
            {
                uniqueUserNames.Add(user.Username);
            }

            if (!uniqueUserNames.Add(request.Username))
            {
                var errorMessage = new { message = $"The user '{request.Username}' already exists." };
                Console.WriteLine($"Returning Error Message: {JsonConvert.SerializeObject(errorMessage)}");
                return errorMessage;
            }

            var hmac = new HMACSHA512();
            User newUser = new User()
            {
                Username = request.Username,
                PasswordSalt = hmac.Key,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password)),
                TokenCreated = DateTime.Now,
                TokenExpires = DateTime.Now
            };
            await _userRepository.AddAsync(newUser);
            string token = await _tokenService.GenerateAccessToken(newUser);

            var successMessage = new { message = token };
            Console.WriteLine($"Returning success message: {JsonConvert.SerializeObject(successMessage)}");
            return successMessage;
        }
    }
}
