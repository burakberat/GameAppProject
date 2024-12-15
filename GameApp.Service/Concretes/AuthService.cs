using GameApp.Infrastructure.Models.Dtos;
using GameApp.Infrastructure.Models.Enums;
using GameApp.Model.Dtos;
using GameApp.Model.Entities;
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

        public AuthService(IConfiguration configuration, IGameRepository gameRepository)
        {
            _configuration = configuration;
            _gameRepository = gameRepository;
        }

        public async Task<ResultDto<string>> LoginAsync(LoginDto item)
        {
            JwtDto jwtDto = new JwtDto();

            var userdto = await _gameRepository.GetProjectAsync<User, UserDto>(d => d.EPosta == item.EPosta);

            if (userdto is null)
            {
                return ResultDto<string>.Error(Messages.UserNotFound);
            }

            userdto.Roles = new List<string>();
            userdto.Roles.Add("admin");

            var token = CreateToken(userdto);

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
    }
}
