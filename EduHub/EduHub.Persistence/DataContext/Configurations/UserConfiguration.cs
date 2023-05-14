using EduHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduHub.Persistence.DataContext.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.Email).IsUnique();

            builder.HasMany(ur => ur.UserRoles)
                .WithOne(u => u.User)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired(false)
                .IsRequired();

            builder.HasMany(x => x.TestResults)
                .WithOne(x => x.Student)
                .HasForeignKey(x => x.StudentId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(x => x.Courses)
                .WithOne(x => x.Teacher)
                .HasForeignKey(x => x.TeacherId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(x => x.Tests)
                .WithOne(x => x.Teacher)
                .HasForeignKey(x => x.TeacherId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(x => x.StudentsCourses)
                .WithOne(x => x.Student)
                .HasForeignKey(x => x.StudentId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.NoAction);

          //  builder.HasData(
          //    new User
          //    {
          //        Id = Guid.Parse("8e445865-a24d-4543-a6c6-9443d048cdb8"),
          //        Email = "admin2@gmail.com",
          //        NormalizedEmail = "admin2@gmail.com".ToUpper(),
          //        UserName = "admin2",
          //        NormalizedUserName = "admin2".ToUpper(),
          //        FirstName = "admin2",
          //        LastName = "admin",
          //        EmailConfirmed = true,
          //        IsBanned = false,
          //        UserImgUrl = null,
          //        PasswordHash = "AQAAAAEAACcQAAAAEDbv2gdENQQEj74VQ3pFfKXFJmAUYXlYNRuIcXMz/qFC2aIFabazxJVkWBgHDCuIvQ==",//Motlox23
          //        SecurityStamp = "2FIUSIDWWXNH7N6KXWVZFFGAICGDPTX7",
          //        ConcurrencyStamp = "3933b6f9-affa-4c92-8691-4a4a1d0e027e",
          //        RegisterTime = DateTimeOffset.UtcNow
          //    }
          //);
        }
    }
}