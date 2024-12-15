using AutoMapper;
using GameApp.Infrastructure.Repositories.Abstracts;
using GameApp.Infrastructure.Repositories.Context;

namespace GameApp.Infrastructure.Repositories.Concretes
{
    public class LogRepository : BaseRepository, ILogRepository
    {
        public LogRepository(LogDBContext context, IMapper mapper) : base(context, mapper)
        {
        }
    }
}
