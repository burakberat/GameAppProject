using GameApp.Infrastructure.Models.Dtos;
using GameApp.Model.Dtos;

namespace GameApp.Service.Abstracts
{
    public interface IRoleService
    {
        //User
        Task<ResultDto<string>> AddUserRoleAsync(AddRoleDto addRoleDto);
        Task<ResultDto<string>> AddRangeUserRoleAsync(List<AddRoleDto> addRoleDtos);
        Task<ResultDto<string>> RemoveUserRoleAsync(int userId, int roleId);

        //Personnel
        Task<ResultDto<string>> AddPersonnelRoleAsync(AddRoleDto addRoleDto);
        Task<ResultDto<string>> RemovePersonnelRoleAsync(int userId, int roleId);


    }
}
