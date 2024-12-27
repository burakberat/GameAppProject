using GameApp.Infrastructure.Repositories.Abstracts;
using GameApp.Model.Dtos.PersonnelDtos;
using GameApp.Model.Dtos.UserDtos;

namespace GameApp.Repository.Abstracts
{
    public interface IGameRepository : IBaseRepository
    {
        List<UserDetailDto> GetUserDetailFromLinq(int userId);
        List<PersonnelDetailDto> GetPersonnelDetailFromLinq(int userId);
    }
}
