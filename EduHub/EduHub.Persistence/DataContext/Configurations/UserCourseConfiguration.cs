using EduHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduHub.Persistence.DataContext.Configurations
{
    public class UserCourseConfiguration : IEntityTypeConfiguration<UserCourse>
    {
        public void Configure(EntityTypeBuilder<UserCourse> builder)
        {
            builder.HasKey(x => new { x.StudentId, x.CourseId });

            builder.HasOne(x => x.Student)
                .WithMany(x => x.StudentsCourses)
                .HasForeignKey(x => x.StudentId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Course)
                .WithMany(x => x.StudentsCourses)
                .HasForeignKey(x => x.CourseId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}