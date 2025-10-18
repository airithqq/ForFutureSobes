using AutoMapper;
using ForFutureSobes.Application.DTOs;
using ForFutureSobes.Model.Domain;

namespace ForFutureSobes.Application.Mapping
{
    public class ThemeMappingProfile : Profile
    {
        public ThemeMappingProfile()
        {
            CreateMap<CreateThemeDTO, Theme>();
        }
    }
}
