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
            CreateMap<Project, ProjectSummaryDto>();
            CreateMap<ProjectSummaryDto, Project>();
            CreateMap<Project, CreateProjectDto>();
            CreateMap<CreateProjectDto, Project>();
            CreateMap<Project, UpdateProjectDto>();
            CreateMap<UpdateProjectDto, Project>();
            CreateMap<ProjectTask, TasksFilterDto>();
            CreateMap<TasksFilterDto, ProjectTask>();
            CreateMap<ProjectTask, UpdateTaskDto>();
            CreateMap<UpdateTaskDto, ProjectTask>();
            CreateMap<Developer, DeveloperWorkloadDto>();
            CreateMap<DeveloperWorkloadDto, Developer>();
            CreateMap<Developer, DeveloperRiskWorkloadDto>();
            CreateMap<DeveloperRiskWorkloadDto, Developer>();
        }
    }
}
