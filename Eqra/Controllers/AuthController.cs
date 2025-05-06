using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Eqra.Data;
using Eqra.Models;
using Eqra.Repositories;
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
        private readonly UserRepository _userRepository;

        public AuthController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromQuery] RegisterDto registerDto)
        {
            object token = await _userRepository.CreateUserAsync(registerDto);

            return Ok("Success");

        }

    }
}
