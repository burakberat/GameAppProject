using GameApp.Infrastructure.Models.Entities;
using GameApp.Infrastructure.Repositories.Abstracts;
using GameApp.Infrastructure.Service.Abstracts;

namespace GameApp.Infrastructure.Service.Concretes
{
    public class LogService: ILogService
    {
        ILogRepository _repository;

        public LogService(ILogRepository repository)
        {
            _repository = repository;
        }

        public async Task AddLogAsync(LogTable log)
        {
            await _repository.AddAsync(log);
            await _repository.SaveChangesAsync();
        }
        public async Task AddErrorLogAsync(ErrorLogTable log)
        {
            await _repository.AddAsync(log);
            await _repository.SaveChangesAsync();
        }
    }
}
