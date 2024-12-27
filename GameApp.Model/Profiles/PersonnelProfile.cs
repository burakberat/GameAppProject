using AutoMapper;
using GameApp.Model.Dtos;
using GameApp.Model.Dtos.PersonnelDtos;
using GameApp.Model.Entities.PersonnelEntities;

namespace GameApp.Model.Profiles
{
    public class PersonnelProfile : Profile
    {
        public PersonnelProfile()
        {
            CreateMap<Personnel, PersonnelDto>()
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.PasswordHash)).ReverseMap();
            CreateMap<Personnel, PersonnelRegisterDto>()
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.PasswordHash)).ReverseMap();
            CreateMap<Personnel, PersonnelDetailDto>();
            CreateMap<PersonnelRoles, AddRoleDto>().ReverseMap();
        }
    }
}
