using AutoMapper;
using GameApp.Infrastructure.Repositories.Concretes;
using GameApp.Model.Dtos.PersonnelDtos;
using GameApp.Model.Dtos.UserDtos;
using GameApp.Model.Entities;
using GameApp.Repository.Abstracts;
using GameApp.Repository.Contexts;

namespace GameApp.Repository.Concretes
{
    public class GameRepository : BaseRepository, IGameRepository
    {
        private readonly GameAppDbContext _context;
        public GameRepository(GameAppDbContext context, IMapper mapper) : base(context, mapper)
        {
            _context = context;
        }

        public List<PersonnelDetailDto> GetPersonnelDetailFromLinq(int userId)
        {            
            var result = (from u in _context.Personnel
                          join ur in _context.PersonnelRoles on u.Id equals ur.UserId
                          join r in _context.PersonnelRole on ur.RoleId equals r.Id
                          where u.Id == userId
                          select new PersonnelDetailDto
                          {
                              Id = u.Id,
                              Name = u.Name,
                              Surname = u.Surname,
                              EPosta = u.EPosta,
                              PasswordHash = u.PasswordHash,
                              Phone = u.Phone,
                              RoleName = r.RoleName
                          }).ToList();
            return result;
        }

        public List<UserDetailDto> GetUserDetailFromLinq(int userId)
        {
            var result = (from u in _context.User
                          join ur in _context.UserRoles on u.Id equals ur.UserId
                          join r in _context.UserRole on ur.RoleId equals r.Id
                          where u.Id == userId
                          select new UserDetailDto
                          {
                              Id = u.Id,
                              Name = u.Name,
                              Surname = u.Surname,
                              EPosta = u.EPosta,
                              PasswordHash = u.PasswordHash,
                              Phone = u.Phone,
                              RoleName = r.RoleName
                          }).ToList();
            return result;
        }
    }
}
