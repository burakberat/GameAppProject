using AutoMapper;
using GameApp.Infrastructure.Repositories.Concretes;
using GameApp.Repository.Contexts;
using GameApp.Repository.Repositories.Abstracts;

namespace GameApp.Repository.Repositories.Concretes
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(GameAppDbContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
