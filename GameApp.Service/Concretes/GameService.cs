using AutoMapper;
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

        public GameService(IGameRepository gameRepository, IMapper mapper)
        {
            _gameRepository = gameRepository;
            _mapper = mapper;
        }

        public async Task<ResultDto<List<GameListDto>>> GetGames()
        {
            var data = await _gameRepository.ListProjectAsync<Games, GameListDto>(d => d.StatusId != 0);
            return ResultDto<List<GameListDto>>.Success(data);
        }
    }
}
