using AutoMapper;
using GameApp.Infrastructure.Repositories.Concretes;
using GameApp.Repository.Abstracts;
using GameApp.Repository.Contexts;

namespace GameApp.Repository.Concretes
{
    public class GameRepository: BaseRepository, IGameRepository
    {
        public GameRepository(GameAppDbContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
