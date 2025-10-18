using AutoMapper;
using ForFutureSobes.Application.DTOs;
using ForFutureSobes.Model.Domain;

namespace ForFutureSobes.Application.Mapping
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

