using AutoMapper;
using ForFutureSobes.Application.DTOs;
using ForFutureSobes.Model.Domain;

namespace ForFutureSobes.Application.Mapping
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
