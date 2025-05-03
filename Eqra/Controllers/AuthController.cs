using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Eqra.Data;
using Eqra.Models;
using Eqra.Services;
using Eqra.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Eqra.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;

        private readonly ApplicationDbContext _context;

        public AuthController(ITokenService tokenService, ApplicationDbContext context)
        {
            _tokenService = tokenService;
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {

            try
            {
                if (await _context.Users.AnyAsync(u => u.Username == registerDto.Username))
                    return BadRequest("User Already Registered");

                using var hmac = new HMACSHA256();

                var user = new User
                {
                    Username = registerDto.Username,
                    PasswordHash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password))),
                    

                };

                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                };

                var accessToken = _tokenService.GenerateAccessToken(claims);
                var refreshToken = _tokenService.GenerateRefreshToken();

                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return Ok("Registration Succesfull");
            }
            catch (Exception ex)
            {
                // Log the exception (logging mechanism not shown here)
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }


        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Username == loginDto.Username);
            if (user == null)
                return Unauthorized("Invalid Username");

            using var hmac = new HMACSHA256();
            var computedHash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password)));
            if (user.PasswordHash != computedHash)
                return BadRequest("Invalid Password");

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var accessToken = _tokenService.GenerateAccessToken(claims);
            var refreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

            await _context.SaveChangesAsync();

            return Ok(new
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            });

        }


        ////Dummy User check
        //[HttpPost("login")]
        //public IActionResult Login([FromBody] LoginDto loginDto)
        //{
        //    // Dummy user check
        //    if (loginDto.Username == "admin" && loginDto.Password == "1234")
        //    {
        //        var token = _tokenService.GenerateToken(loginDto.Username);
        //        return Ok(new { token });
        //    }
        //    return Unauthorized();
        //}



        [HttpGet("refresh")]
        public async Task<IActionResult> Refresh(TokenRequestDto tokenRequest)
        {
            var principal = _tokenService.GetPrincipalFormExpiredToken(tokenRequest.AccessToken);
            var username = principal.Identity.Name;
            
            var user = await _context.Users.SingleOrDefaultAsync(x => x.Username == username);

            if(user == null || user.RefreshToken != tokenRequest.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow) 
                return Unauthorized();

            var newAccessToken = _tokenService.GenerateAccessToken(principal.Claims);
            var newRefreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            await _context.SaveChangesAsync();
            return Ok(new
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            });

        }


        [HttpGet("protected")]
        [Authorize]
        public IActionResult Protected()
        {
            var username = User.Identity.Name;
            return Ok($"Hello {username}, you are authenticated!");
        }
    }
}
