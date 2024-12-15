using AutoMapper;
using GameApp.Model.Dtos;
using GameApp.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameApp.Model.Profiles
{
    public class GameAppProfile : Profile
    {
        public GameAppProfile()
        {
            CreateMap<Games, GameListDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));
            CreateMap<Games, GameDetailDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));
            CreateMap<Games, GameFormDto>().ReverseMap();

            CreateMap<User, UserDto>();
        }
    }
}
