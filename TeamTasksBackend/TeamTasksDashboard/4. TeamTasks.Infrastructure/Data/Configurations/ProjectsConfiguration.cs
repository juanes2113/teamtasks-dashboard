using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using _3._TeamTasks.Domain.Models;

namespace _4._TeamTasks.Infrastructure.Data.Configurations
{
    public class ProjectsConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> entity)
        {
            entity.HasKey(e => e.Projectid).HasName("projects_pkey");

            entity.ToTable("projects");

            entity.Property(e => e.Projectid).HasColumnName("projectid");
            entity.Property(e => e.Clientname)
                .HasMaxLength(150)
                .HasColumnName("clientname");
            entity.Property(e => e.Enddate).HasColumnName("enddate");
            entity.Property(e => e.Name)
                .HasMaxLength(150)
                .HasColumnName("name");
            entity.Property(e => e.Startdate).HasColumnName("startdate");
        }
    }
}
