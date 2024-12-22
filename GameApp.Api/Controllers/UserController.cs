using GameApp.Model.Dtos;
using GameApp.Service.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace GameApp.Api.Controllers
{
    //[Authorize(Roles = "admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Registerrrr([FromBody] UserRegisterDto userDto)
        {
            var data = await _userService.RegisterAsync(userDto);
            return Ok(data);
        }
    }
}
