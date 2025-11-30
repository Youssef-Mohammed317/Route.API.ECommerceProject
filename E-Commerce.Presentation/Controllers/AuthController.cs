using E_Commerce.Service.Abstraction.Interfaces;
using E_Commerce.Shared.DTOs.AuthDTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            var userDTO = await _authService.LoginAsync(loginDTO);
            if (userDTO == null)
            {
                return Unauthorized("Invalid credentials");
            }
            return Ok(userDTO);
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {
            var userDTO = await _authService.RegisterAsync(registerDTO);
            if (userDTO == null)
            {
                return BadRequest("Registration failed");
            }
            return Ok(userDTO);
        }
    }
}
