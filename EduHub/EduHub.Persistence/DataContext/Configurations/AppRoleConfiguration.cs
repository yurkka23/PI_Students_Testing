using EduHub.Domain.Constants;
using EduHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduHub.Persistence.DataContext.Configurations
{
    public class AppRoleConfiguration : IEntityTypeConfiguration<AppRole>
    {
        public void Configure(EntityTypeBuilder<AppRole> builder)
        {
            builder.HasMany(ur => ur.UserRoles)
                .WithOne(u => u.Role)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();

            //seed
            builder.HasData(new AppRole
            {
                Id = Guid.NewGuid(),
                Name = RoleConstants.AdminRole,
                NormalizedName = RoleConstants.AdminRole.ToUpper()
            });

            builder.HasData(new AppRole
            {
                Id = Guid.NewGuid(),
                Name = RoleConstants.StudentRole,
                NormalizedName = RoleConstants.StudentRole.ToUpper()
            });

            builder.HasData(new AppRole
            {
                Id = Guid.NewGuid(),
                Name = RoleConstants.TeacherRole,
                NormalizedName = RoleConstants.TeacherRole.ToUpper()
            });
        }
    }
}