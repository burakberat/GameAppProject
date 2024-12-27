using AutoMapper;
using GameApp.Model.Dtos;
using GameApp.Model.Dtos.UserDtos;
using GameApp.Model.Entities;
using GameApp.Model.Entities.UserEntities;

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
        }
    }
}
