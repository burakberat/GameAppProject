using AutoMapper;
using GameApp.Model.Dtos.UserDtos;
using GameApp.Model.Dtos;
using GameApp.Model.Entities.UserEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameApp.Model.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>().ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.PasswordHash)).ReverseMap();
            CreateMap<User, UserRegisterDto>().ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.PasswordHash)).ReverseMap();
            CreateMap<User, UserDetailDto>();
            CreateMap<User, UserRoles>().ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.Id)).ReverseMap();
            CreateMap<UserRoles, AddRoleDto>().ReverseMap();
        }
    }
}
