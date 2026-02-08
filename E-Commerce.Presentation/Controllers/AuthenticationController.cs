using E_Commerce.Service.Abstraction.Interfaces;
using E_Commerce.Shared.Common;
using E_Commerce.Shared.DTOs.AuthDTOs;
using E_Commerce.Shared.DTOs.OrderDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace E_Commerce.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ApiBaseController
    {
        private readonly IAuthService _authService;

        public AuthenticationController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var result = await _authService.LoginAsync(loginDTO);
            // service should return ErrorType.InvalidCrendentials on bad login
            return FromResult(result);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var result = await _authService.RegisterAsync(registerDTO);
            // on failure, service returns Validation/Failure errors
            return FromResult(result);
        }
        [HttpGet("currentUser")]
        [Authorize]
        public async Task<IActionResult> GetCurrentUser()
        {

            var result = await _authService.GetUserByEmail(GetEmailFromToken());
            return FromResult(result);
        }
        [HttpGet("emailExists")]
        public async Task<IActionResult> CheckEmailExists([FromQuery] string email)
        {
            var result = await _authService.CheckEmailExist(email);
            return Ok(result.Value);
            //return FromResult(result);
        }
        [HttpGet("address")]
        [Authorize]
        public async Task<IActionResult> GetAddress()
        {

            var result = await _authService.GetCurrentUserAddress(GetEmailFromToken());
            return FromResult(result);

        }
        [Authorize]
        [HttpPut("address")]
        public async Task<IActionResult> UpdateAddress(ShippingAddressDto shippingAddressDto)
        {
            var result = await _authService.CreateOrUpdateAddresss(shippingAddressDto, GetEmailFromToken());
            return FromResult(result);
        }
        [Authorize]
        [HttpPost("address")]
        public async Task<IActionResult> CreateAddress(ShippingAddressDto shippingAddressDto)
        {
            var result = await _authService.CreateOrUpdateAddresss(shippingAddressDto, GetEmailFromToken());
            return FromResult(result);
        }
    }
}
