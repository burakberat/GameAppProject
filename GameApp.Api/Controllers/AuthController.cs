using GameApp.Infrastructure.Models.Dtos;
using GameApp.Infrastructure.Models.Enums;
using GameApp.Model.Dtos;
using GameApp.Service.Abstracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GameApp.Api.Controllers
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

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return Ok(ResultDto<JwtDto>.Error(Messages.ModelNotValid));
            }
            var result = _authService.LoginAsync(loginDto);
            return Ok(result);
        }
    }
}
