using Eqra.Features.DataAccess.Repositories;
using Eqra.Features.DomainModels;
using Eqra.Features.ServiceImplementation;
using Eqra.Features.ServiceImplementation.ServiceInterface;
using Microsoft.AspNetCore.Mvc;

namespace Eqra.Features.Application
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromQuery] RegisterDto registerDto)
        {
            object token = await _authService.CreateUserAsync(registerDto);

            return Ok("Success");

        }

    }
}
