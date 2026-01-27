using Microsoft.Extensions.DependencyInjection;
using _2._TeamTasks.Application.Interfaces;
using _2._TeamTasks.Application.Services;
using _3._TeamTasks.Domain.Validators;
using _3._TeamTasks.Domain.Dtos;
using FluentValidation;

namespace _2._TeamTasks.Application
{
    public static class ServiceCollection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IDeveloperApplication, DeveloperApplication>();

            services.AddScoped<IValidator<CreateDeveloperDto>, CreateDeveloperDtoValidator>();
            services.AddScoped<IValidator<UpdateDeveloperDto>, UpdateDeveloperDtoValidator>();

            return services;
        }
    }
}
