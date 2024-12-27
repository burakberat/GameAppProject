using GameApp.Infrastructure.Models.Dtos;
using GameApp.Model.Dtos;
using GameApp.Model.Dtos.PersonnelDtos;
using GameApp.Model.Dtos.UserDtos;

namespace GameApp.Service.Abstracts
{
    public interface IAuthService
    {        
        Task<ResultDto<PersonnelRegisterDto>> PersonnelRegisterAsync(PersonnelRegisterDto personnelDto);
        Task<ResultDto<string>> PersonnelLoginAsync(LoginDto item);
        Task<ResultDto<UserRegisterDto>> UserRegisterAsync(UserRegisterDto userDto);
        Task<ResultDto<string>> UserLoginAsync(LoginDto item);
    }
}
