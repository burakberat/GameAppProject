using GameApp.Infrastructure.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameApp.Infrastructure.Service.Abstracts
{
    public interface ILogService
    {
        Task AddLogAsync(LogTable log);
        Task AddErrorLogAsync(ErrorLogTable log);
    }
}
