using Microsoft.EntityFrameworkCore;
using _3._TeamTasks.Domain.Models;
using _3._TeamTasks.Domain.Enums;

namespace _4._TeamTasks.Infrastructure.Data;

public partial class TeamTasksDbContext : DbContext
{
    public TeamTasksDbContext(DbContextOptions<TeamTasksDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Developer> Developers { get; set; }
    public virtual DbSet<Project> Projects { get; set; }
    public virtual DbSet<ProjectTask> Tasks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresEnum<ProjectStatus>("project_status");
        modelBuilder.HasPostgresEnum<TaskPriority>("task_priority");
        modelBuilder.HasPostgresEnum<TasksStatus>("task_status");

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TeamTasksDbContext).Assembly);
    }
}
