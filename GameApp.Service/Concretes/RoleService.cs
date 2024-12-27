using AutoMapper;
using GameApp.Infrastructure.Models.Dtos;
using GameApp.Infrastructure.Models.Enums;
using GameApp.Model.Dtos;
using GameApp.Model.Entities.PersonnelEntities;
using GameApp.Model.Entities.UserEntities;
using GameApp.Repository.Abstracts;
using GameApp.Service.Abstracts;

namespace GameApp.Service.Concretes
{
    public class RoleService : IRoleService
    {
        private readonly IGameRepository _gameRepository;
        private readonly IMapper _mapper;
        public RoleService(IGameRepository gameRepository, IMapper mapper)
        {
            _gameRepository = gameRepository;
            _mapper = mapper;
        }

        public async Task<ResultDto<string>> AddUserRoleAsync(AddRoleDto addRoleDto)
        {
            var data = _mapper.Map<UserRoles>(addRoleDto);
            data.LastTransactionDate = DateTime.UtcNow;
            await _gameRepository.AddAsync(data);
            await _gameRepository.SaveChangesAsync();
            return ResultDto<string>.Success(Messages.Success);
        }

        public async Task<ResultDto<string>> AddRangeUserRoleAsync(List<AddRoleDto> addRoleDtos)
        {
            var data = _mapper.Map<List<UserRoles>>(addRoleDtos);
            data.ForEach(d => d.LastTransactionDate = DateTime.UtcNow);
            await _gameRepository.AddRangeAsync(data);
            await _gameRepository.SaveChangesAsync();
            return ResultDto<string>.Success(Messages.Success);
        }

        public async Task<ResultDto<string>> RemoveUserRoleAsync(int userId, int roleId)
        {
            var data = await _gameRepository.GetAsync<UserRoles>(d => d.UserId == userId && d.RoleId == roleId && d.StatusId == 1);
            await _gameRepository.DeleteAsync(data);
            await _gameRepository.SaveChangesAsync();
            return ResultDto<string>.Success(Messages.Success);
        }

        public async Task<ResultDto<string>> AddPersonnelRoleAsync(AddRoleDto addRoleDto)
        {
            var data = _mapper.Map<PersonnelRoles>(addRoleDto);
            data.LastTransactionDate = DateTime.UtcNow;
            await _gameRepository.AddAsync(data);
            await _gameRepository.SaveChangesAsync();
            return ResultDto<string>.Success(Messages.Success);
        }

        public async Task<ResultDto<string>> RemovePersonnelRoleAsync(int userId, int roleId)
        {
            var data = await _gameRepository.GetAsync<PersonnelRoles>(d => d.UserId == userId && d.RoleId == roleId && d.StatusId == 1);
            await _gameRepository.DeleteAsync(data);
            await _gameRepository.SaveChangesAsync();
            return ResultDto<string>.Success(Messages.Success);
        }
    }
}
