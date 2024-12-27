using GameApp.Infrastructure.Models.Dtos;
using GameApp.Infrastructure.Models.Enums;
using GameApp.Model.Dtos;
using GameApp.Model.Dtos.PersonnelDtos;
using GameApp.Model.Dtos.UserDtos;
using GameApp.Service.Abstracts;
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

        [HttpPost("personnel/register")]
        public async Task<IActionResult> Register([FromBody] PersonnelRegisterDto personnelDto)
        {
            if (!ModelState.IsValid)
            {
                return Ok(ResultDto<string>.Error(Messages.ModelNotValid));
            }
            var data = await _authService.PersonnelRegisterAsync(personnelDto);
            return Ok(data);
        }
        [HttpPost("personnel/login")]
        public async Task<IActionResult> PersonnelLogin([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return Ok(ResultDto<JwtDto>.Error(Messages.ModelNotValid));
            }
            var result = await _authService.PersonnelLoginAsync(loginDto);
            return Ok(result);
        }
        [HttpPost("user/register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto userDto)
        {
            var data = await _authService.UserRegisterAsync(userDto);
            return Ok(data);
        }
        [HttpPost("user/login")]
        public async Task<IActionResult> UserLogin([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return Ok(ResultDto<JwtDto>.Error(Messages.ModelNotValid));
            }
            var result = await _authService.UserLoginAsync(loginDto);
            return Ok(result);
        }
    }
}
