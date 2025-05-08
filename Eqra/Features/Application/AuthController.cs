using Eqra.Features.DomainModels;
using Eqra.Features.ServiceImplementation.ServiceInterface;
using Microsoft.AspNetCore.Mvc;

namespace Eqra.Features.Application
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRegisterService _registerService;
        private readonly IUserLoginService _loginService;

        public AuthController(IUserRegisterService registerService, IUserLoginService loginService)
        {
            _registerService = registerService;
            _loginService = loginService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromQuery] RegisterDto registerDto)
        {
            var response = await _registerService.CreateUserAsync(registerDto);
            

            // Uncomment the following lines if you want to return the token or success message
            //string token = await _tokenService.GenerateAccessToken(user);
            //var successMessage = new { message = token };
            //Console.WriteLine($"Returning success message: {JsonConvert.SerializeObject(successMessage)}");

            return Ok(response);

        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromQuery] LoginDto loginDto)
        {
            var response = await _loginService.LoginUserAsync(loginDto);
            
            return Ok(response);
        }

    }
}
