using GameApp.Infrastructure.Models.Dtos;
using GameApp.Model.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameApp.Service.Abstracts
{
    public interface IAuthService
    {
        Task<ResultDto<string>> LoginAsync(LoginDto item);
    }
}
