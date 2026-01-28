using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using _3._TeamTasks.Domain.Models;

namespace _4._TeamTasks.Infrastructure.Data.Configurations
{
    public class TasksConfiguration : IEntityTypeConfiguration<ProjectTask>
    {
        public void Configure(EntityTypeBuilder<ProjectTask> entity)
        {
            entity.HasKey(e => e.Taskid).HasName("tasks_pkey");

            entity.ToTable("tasks");

            entity.Property(e => e.Taskid).HasColumnName("taskid");
            entity.Property(e => e.Assigneeid).HasColumnName("assigneeid");
            entity.Property(e => e.Completiondate).HasColumnName("completiondate");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Duedate).HasColumnName("duedate");
            entity.Property(e => e.Estimatedcomplexity).HasColumnName("estimatedcomplexity");
            entity.Property(e => e.Priority).HasColumnName("priority");
            entity.Property(e => e.Projectid).HasColumnName("projectid");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.Title)
                .HasMaxLength(200)
                .HasColumnName("title");

            entity.HasOne(d => d.Assignee).WithMany(p => p.ProjectTasks)
                .HasForeignKey(d => d.Assigneeid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_task_assignee");

            entity.HasOne(d => d.Project).WithMany(p => p.ProjectTasks)
                .HasForeignKey(d => d.Projectid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_task_project");
        }
    }
}
