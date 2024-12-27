using AutoMapper;
using GameApp.Infrastructure.Hashing;
using GameApp.Infrastructure.Models.Dtos;
using GameApp.Infrastructure.Models.Enums;
using GameApp.Model.Dtos;
using GameApp.Model.Dtos.PersonnelDtos;
using GameApp.Model.Dtos.UserDtos;
using GameApp.Model.Entities.PersonnelEntities;
using GameApp.Model.Entities.UserEntities;
using GameApp.Repository.Abstracts;
using GameApp.Service.Abstracts;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GameApp.Service.Concretes
{
    public class AuthService : IAuthService
    {
        IConfiguration _configuration;
        private readonly IGameRepository _gameRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IMapper _mapper;

        public AuthService(IConfiguration configuration, IGameRepository gameRepository, IPasswordHasher passwordHasher, IMapper mapper)
        {
            _configuration = configuration;
            _gameRepository = gameRepository;
            _passwordHasher = passwordHasher;
            _mapper = mapper;
        }
        public async Task<ResultDto<PersonnelRegisterDto>> PersonnelRegisterAsync(PersonnelRegisterDto personnelDto)
        {
            if (await _gameRepository.GetProjectAsync<Personnel, PersonnelRegisterDto>(x => x.EPosta == personnelDto.EPosta) != null)
            {
                throw new Exception("Personnel already exists");
            }

            var data = _mapper.Map<Personnel>(personnelDto);
            data.PasswordHash = _passwordHasher.Hash(personnelDto.Password);
            data.LastTransactionDate = DateTime.UtcNow;

            await _gameRepository.AddAsync(data);
            await _gameRepository.SaveChangesAsync();
            return ResultDto<PersonnelRegisterDto>.Success(personnelDto);
        }

        public async Task<ResultDto<string>> PersonnelLoginAsync(LoginDto item)
        {
            JwtDto jwtDto = new JwtDto();

            var personnelDto = await _gameRepository.GetProjectAsync<Personnel, PersonnelDto>(d => d.EPosta == item.EPosta);
            var personnelDetailDto = _gameRepository.GetPersonnelDetailFromLinq(personnelDto.Id);

            if (personnelDto is null)
            {
                return ResultDto<string>.Error(Messages.UserNotFound);
            }
            bool verifiedPassword = _passwordHasher.Verify(item.Password, personnelDto.Password);

            if (!verifiedPassword)
            {
                return ResultDto<string>.Error(Messages.PasswordError);
            }

            personnelDto.Roles = new List<string>();
            for (var i = 0; i < personnelDetailDto.Count; i++)
            {
                personnelDto.Roles.Add(personnelDetailDto[i].RoleName);
            }
            var token = CreateToken(personnelDto);

            jwtDto.Token = token;

            return ResultDto<string>.Success(token);
        }

        public async Task<ResultDto<UserRegisterDto>> UserRegisterAsync(UserRegisterDto userDto)
        {
            if (await _gameRepository.GetProjectAsync<User, UserRegisterDto>(x => x.EPosta == userDto.EPosta) != null)
            {
                throw new Exception("User already exists");
            }

            var data = _mapper.Map<User>(userDto);
            data.PasswordHash = _passwordHasher.Hash(userDto.Password);
            data.LastTransactionDate = DateTime.UtcNow;
            await _gameRepository.AddAsync(data);

            //var user = await _gameRepository.GetProjectAsync<User, UserRegisterDto>(x => x.EPosta == userDto.EPosta);
            //var roleData = _mapper.Map<UserRoles>(user);
            //roleData.RoleId = 1;            
            //await _gameRepository.AddAsync(roleData);

            await _gameRepository.SaveChangesAsync();
            return ResultDto<UserRegisterDto>.Success(userDto);
        }

        public async Task<ResultDto<string>> UserLoginAsync(LoginDto item)
        {
            JwtDto jwtDto = new JwtDto();

            var userDto = await _gameRepository.GetProjectAsync<User, UserDto>(d => d.EPosta == item.EPosta);
            var userDetailDto = _gameRepository.GetUserDetailFromLinq(userDto.Id);

            if (userDto is null)
            {
                return ResultDto<string>.Error(Messages.UserNotFound);
            }
            bool verifiedPassword = _passwordHasher.Verify(item.Password, userDto.Password);

            if (!verifiedPassword)
            {
                return ResultDto<string>.Error(Messages.PasswordError);
            }

            userDto.Roles = new List<string>();
            for (var i = 0; i < userDetailDto.Count; i++)
            {
                userDto.Roles.Add(userDetailDto[i].RoleName);
            }
            var token = CreateToken(userDto);

            jwtDto.Token = token;

            return ResultDto<string>.Success(token);
        }

        private string CreateToken(UserDto user)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings").Get<JwtSettings>();
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey));
            var crendentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddMinutes(jwtSettings.ExpiryMinutes);

            var claims = new List<Claim>();
            claims.Add(new Claim("id", user.Id.ToString()));
            claims.Add(new Claim("name", user.Name));
            claims.Add(new Claim("surname", user.Surname));
            foreach (var item in user.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, item));

            }

            var token = new JwtSecurityToken(
                issuer: jwtSettings.Issuer,
                audience: jwtSettings.Audience,
                signingCredentials: crendentials,
                expires: expires,
                claims: claims
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string CreateToken(PersonnelDto personnelDto)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings").Get<JwtSettings>();
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey));
            var crendentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddMinutes(jwtSettings.ExpiryMinutes);

            var claims = new List<Claim>();
            claims.Add(new Claim("id", personnelDto.Id.ToString()));
            claims.Add(new Claim("name", personnelDto.Name));
            claims.Add(new Claim("surname", personnelDto.Surname));
            foreach (var item in personnelDto.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, item));

            }

            var token = new JwtSecurityToken(
                issuer: jwtSettings.Issuer,
                audience: jwtSettings.Audience,
                signingCredentials: crendentials,
                expires: expires,
                claims: claims
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
