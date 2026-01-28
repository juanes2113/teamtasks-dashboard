using Request.Infrastructure.Persistence.Mappings;
using _4._TeamTasks.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using _4._TeamTasks.Infrastructure.Data;
using _3._TeamTasks.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace _4._TeamTasks.Infrastructure
{
    public static class ServiceCollection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TeamTasksDbContext>(options =>
                options.UseNpgsql(
                    configuration.GetConnectionString("DefaultConnection")
                )
            );

            services.AddAutoMapper(typeof(AutoMapperProfile));

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IDeveloperRepository, DeveloperRepository>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IProjectTaskRepository, ProjectTaskRepository>();

            return services;
        }
    }
}
