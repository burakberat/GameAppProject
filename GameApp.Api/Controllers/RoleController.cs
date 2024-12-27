using GameApp.Model.Dtos;
using GameApp.Service.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameApp.Api.Controllers
{
    //[Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpPost("user/addrole")]
        public async Task<IActionResult> AddUserRoleAsync([FromBody] AddRoleDto addRoleDto)
        {
            var data = await _roleService.AddUserRoleAsync(addRoleDto);
            return Ok(data);
        }

        [HttpPost("user/addrangerole")]
        public async Task<IActionResult> AddRangeUserRoleAsync([FromBody] List<AddRoleDto> addRoleDtos)
        {
            var data = await _roleService.AddRangeUserRoleAsync(addRoleDtos);
            return Ok(data);
        }

        [HttpPost("personnel/addrole")]
        public async Task<IActionResult> AddPersonnelRoleAsync([FromBody] AddRoleDto addRoleDto)
        {
            var data = await _roleService.AddPersonnelRoleAsync(addRoleDto);
            return Ok(data);
        }

        [HttpDelete("personnel/role/delete")]
        public async Task<IActionResult> RemovePersonnelRoleAsync(int userId, int roleId)
        {
            var data = await _roleService.RemovePersonnelRoleAsync(userId, roleId);
            return Ok(data);
        }

        [HttpDelete("user/role/delete")]
        public async Task<IActionResult> RemoveUserRoleAsync(int userId, int roleId)
        {
            var data = await _roleService.RemoveUserRoleAsync(userId, roleId);  
            return Ok(data);
        }
    }
}
