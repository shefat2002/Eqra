using System.Security.Cryptography;
using System.Text;
using Eqra.Controllers;
using Eqra.Data;
using Eqra.Models;
using Eqra.Services.Interfaces;
using Newtonsoft.Json;

namespace Eqra.Repositories
{
    public class UserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ITokenService _tokenService;

        public UserRepository(ApplicationDbContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        public async Task<object> CreateUserAsync(RegisterDto request)
        {
            //var users = await _context.f;
            //var uniqueUserNames = new HashSet<string>();
            //foreach (var user in users)
            //{
            //    uniqueUserNames.Add(user.Name);
            //}

            //if (!uniqueUserNames.Add(request.Username))
            //{
            //    var errorMessage = new { message = $"The user '{request.Username}' already exists." };
            //    Console.WriteLine($"Returning Error Message: {JsonConvert.SerializeObject(errorMessage)}");
            //    return errorMessage;
            //}

            var hmac = new HMACSHA512();
            User newUser = new User()
            {
                Username = request.Username,
                PasswordSalt = hmac.Key,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password)),
                TokenCreated = DateTime.Now,
                TokenExpires = DateTime.Now
            };
            await _context.AddAsync(newUser);
            string token = await _tokenService.GenerateAccessToken(newUser);

            var successMessage = new { message = token };
            Console.WriteLine($"Returning success message: {JsonConvert.SerializeObject(successMessage)}");
            return successMessage;
        }
    }
}
