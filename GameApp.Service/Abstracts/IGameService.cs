using GameApp.Infrastructure.Models.Dtos;
using GameApp.Model.Dtos;

namespace GameApp.Service.Abstracts
{
    public interface IGameService
    {
        Task<ResultDto<List<GameListDto>>> GetGamesAsync();
        Task<ResultDto<GameDetailDto>> GetGameDetailAsync(int id);
        Task<ResultDto<GameFormDto>> AddGameAsync(GameFormDto gameFormDto);
    }
}
