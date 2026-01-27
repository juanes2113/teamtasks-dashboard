using _3._TeamTasks.Domain.Models;
using _3._TeamTasks.Domain.Dtos;
using AutoMapper;

namespace Request.Infrastructure.Persistence.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Developer, CreateDeveloperDto>();
            CreateMap<CreateDeveloperDto, Developer>();
            CreateMap<Developer, UpdateDeveloperDto>();
            CreateMap<UpdateDeveloperDto, Developer>();
        }
    }
}
