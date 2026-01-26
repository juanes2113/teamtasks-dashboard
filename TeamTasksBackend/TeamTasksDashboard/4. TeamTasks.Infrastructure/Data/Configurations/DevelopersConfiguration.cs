using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using _3._TeamTasks.Domain.Models;

namespace _4._TeamTasks.Infrastructure.Data.Configurations
{
    public class DevelopersConfiguration : IEntityTypeConfiguration<Developer>
    {
        public void Configure(EntityTypeBuilder<Developer> entity)
        {
            entity.HasKey(e => e.Developerid).HasName("developers_pkey");

            entity.ToTable("developers");

            entity.HasIndex(e => e.Email, "developers_email_key").IsUnique();

            entity.Property(e => e.Developerid).HasColumnName("developerid");
            entity.Property(e => e.Createdat)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("createdat");
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .HasColumnName("email");
            entity.Property(e => e.Firstname)
                .HasMaxLength(100)
                .HasColumnName("firstname");
            entity.Property(e => e.Isactive)
                .HasDefaultValue(true)
                .HasColumnName("isactive");
            entity.Property(e => e.Lastname)
                .HasMaxLength(100)
                .HasColumnName("lastname");
        }
    }
}
