using GameApp.Infrastructure.Models.Dtos;
using GameApp.Model.Dtos;

namespace GameApp.Service.Abstracts
{
    public interface IUserService
    {
        Task<ResultDto<UserRegisterDto>> RegisterAsync(UserRegisterDto userDto);
    }
}
