using AutoMapper;
using GameApp.Infrastructure.Hashing;
using GameApp.Infrastructure.Models.Dtos;
using GameApp.Model.Dtos;
using GameApp.Model.Entities;
using GameApp.Repository.Repositories.Abstracts;
using GameApp.Service.Abstracts;

namespace GameApp.Service.Concretes
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IPasswordHasher passwordHasher, IMapper mapper)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _mapper = mapper;
        }

        //public sealed record Request(string EPosta, string Name, string Surname, string Password);
        public async Task<ResultDto<UserRegisterDto>> RegisterAsync(UserRegisterDto userDto)
        {
            if (await _userRepository.GetProjectAsync<User, UserRegisterDto>(x => x.EPosta == userDto.EPosta) != null)
            {
                throw new Exception("User already exists");
            }

            var data = _mapper.Map<User>(userDto);
            data.PasswordHash = _passwordHasher.Hash(userDto.Password);
            data.LastTransactionDate = DateTime.UtcNow;

            await _userRepository.AddAsync(data);
            await _userRepository.SaveChangesAsync();
            return ResultDto<UserRegisterDto>.Success(userDto);
        }
    }
}
