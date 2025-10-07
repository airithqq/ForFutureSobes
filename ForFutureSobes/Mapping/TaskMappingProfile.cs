using System.Runtime.InteropServices;
using AutoMapper;
using ForFutureSobes.Domain;
using ForFutureSobes.DTOs;

namespace ForFutureSobes.Mapping
{
    public class TaskMappingProfile : Profile
    {
        public TaskMappingProfile() {

            CreateMap<TaskEntity, ResponseDTO>()
                .ForMember(dest => dest.ThemeName,
                opt => opt.MapFrom(src => src.Theme.Name));
            CreateMap<CreateTaskDTO, TaskEntity>();
        }
    }
}

