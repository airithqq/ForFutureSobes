using ForFutureSobes.Domain;
using ForFutureSobes.DTOs;
using AutoMapper;

namespace ForFutureSobes.Mapping
{
    public class ThemeMappingProfile : Profile
    {
        public ThemeMappingProfile()
        {
            CreateMap<CreateThemeDTO, Theme>();
        }
    }
}
