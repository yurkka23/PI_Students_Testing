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
                Id = Guid.Parse("33A0EF2C-7DA6-45A6-AE36-C9E3DCE66C66"),
                Name = RoleConstants.AdminRole,
                NormalizedName = RoleConstants.AdminRole.ToUpper()
            });

            builder.HasData(new AppRole
            {
                Id = Guid.Parse("B5314384-A039-4A7E-B896-8016203CFE58"),
                Name = RoleConstants.StudentRole,
                NormalizedName = RoleConstants.StudentRole.ToUpper()
            });

            builder.HasData(new AppRole
            {
                Id = Guid.Parse("85CA209B-5904-4135-9EE4-833AB5900E70"),
                Name = RoleConstants.TeacherRole,
                NormalizedName = RoleConstants.TeacherRole.ToUpper()
            });
        }
    }
}