using EduHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduHub.Persistence.DataContext.Configurations
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasMany(ur => ur.Tests)
                .WithOne(u => u.Course)
                .HasForeignKey(ur => ur.CourseId)
                .IsRequired(false)
                .IsRequired();

            builder.HasMany(x => x.StudentsCourses)
                .WithOne(x => x.Course)
                .HasForeignKey(x => x.CourseId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}