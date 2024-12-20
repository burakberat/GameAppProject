using GameApp.Infrastructure.Models.Dtos;
using GameApp.Model.Dtos;

namespace GameApp.Service.Abstracts
{
    public interface IAuthService
    {
        Task<ResultDto<string>> LoginAsync(LoginDto item);
    }
}
