using Microsoft.Extensions.DependencyInjection;
using _2._TeamTasks.Application.Interfaces;
using _2._TeamTasks.Application.Validators;
using _2._TeamTasks.Application.Services;
using _3._TeamTasks.Domain.Dtos;
using FluentValidation;

namespace _2._TeamTasks.Application
{
    public static class ServiceCollection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IDeveloperApplication, DeveloperApplication>();
            services.AddScoped<IProjectApplication, ProjectApplication>();
            services.AddScoped<IProjectTaskApplication, ProjectTaskApplication>();
            services.AddScoped<IDashboardApplication, DashboardApplication>();

            services.AddScoped<IValidator<CreateDeveloperDto>, CreateDeveloperDtoValidator>();
            services.AddScoped<IValidator<UpdateDeveloperDto>, UpdateDeveloperDtoValidator>();
            services.AddScoped<IValidator<CreateProjectDto>, CreateProjectDtoValidator>();
            services.AddScoped<IValidator<UpdateProjectDto>, UpdateProjectDtoValidator>();
            services.AddScoped<IValidator<TasksFilterDto>, TasksFilterDtoValidator>();
            services.AddScoped<IValidator<CreateTaskDto>, CreateTaskDtoValidator>();
            services.AddScoped<IValidator<UpdateTaskDto>, UpdateTaskDtoValidator>();

            return services;
        }
    }
}
