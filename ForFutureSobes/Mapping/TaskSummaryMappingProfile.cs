using AutoMapper;
using ForFutureSobes.Domain;
using ForFutureSobes.DTOs;

namespace ForFutureSobes.Mapping
{
    public class TaskSummaryMappingProfile : Profile 
    {
        public TaskSummaryMappingProfile()
        {
            CreateMap<TaskEntity, TaskSummaryDTO>()
                .ForMember(dest => dest.Theme, opt => opt.MapFrom(src => src.Theme.Name));
        }

    }
}
