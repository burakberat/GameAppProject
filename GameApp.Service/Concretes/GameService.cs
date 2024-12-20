using AutoMapper;
using GameApp.Infrastructure.Cache;
using GameApp.Infrastructure.Models.Dtos;
using GameApp.Model.Dtos;
using GameApp.Model.Entities;
using GameApp.Repository.Abstracts;
using GameApp.Service.Abstracts;

namespace GameApp.Service.Concretes
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;

        public GameService(IGameRepository gameRepository, IMapper mapper, ICacheService cacheService)
        {
            _gameRepository = gameRepository;
            _mapper = mapper;
            _cacheService = cacheService;
        }

        public async Task<ResultDto<GameFormDto>> AddGameAsync(GameFormDto gameFormDto)
        {
            var data = _mapper.Map<Games>(gameFormDto);
            data.LastTransactionDate = DateTime.UtcNow;

            await _gameRepository.AddAsync(data);
            await _gameRepository.SaveChangesAsync();
            return ResultDto<GameFormDto>.Success(gameFormDto);
        }

        public async Task<ResultDto<GameDetailDto>> GetGameDetailAsync(int id)
        {
            var data = await _gameRepository.GetProjectAsync<Games, GameDetailDto>(d => d.Id == id);
            return ResultDto<GameDetailDto>.Success(data);
        }

        public async Task<ResultDto<List<GameListDto>>> GetGamesAsync()
        {
            var data = await _cacheService.GetObjectAsync<List<GameListDto>>("GameList");
            if (data == null)
            {
                data = await _gameRepository.ListProjectAsync<Games, GameListDto>(d => d.StatusId != 0);
                await _cacheService.SetObjectAsync("GameList", data, 60);
            }
            return ResultDto<List<GameListDto>>.Success(data);
        }
    }
}
