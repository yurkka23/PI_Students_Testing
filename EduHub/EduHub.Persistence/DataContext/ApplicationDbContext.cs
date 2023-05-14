using System.Reflection;
using System.Security.Claims;
using EduHub.Domain.Constants;
using EduHub.Domain.Entities;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EduHub.Persistence.DataContext
{
    public class ApplicationDbContext : IdentityDbContext<User, AppRole, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        //public DbSet<User> Users { get; set; }
        public DbSet<AnswerOption> AnswerOptions { get; set; }
        public DbSet<AppRole> AppRoles { get; set; }
        public DbSet<AppUserRole> AppUserRoles { get; set; }
        public DbSet<Course> Course { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<TestResult> TestResults { get; set; }
        public DbSet<UserCourse> UserCourses { get; set; }
        public DbSet<TeacherRequest> TeacherRequests { get; set; }
        public DbSet<PassingTest> PassingTests { get; set; }
        public DbSet<QuestionAnswer> QuestionAnswers { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        //    builder.Entity<IdentityUserRole<Guid>>().HasData(
        //  new IdentityUserRole<Guid>
        //  {
        //      RoleId = Guid.Parse("1CFBA6F6-50FA-4304-9140-4BD86F5A5885"),
        //      UserId = Guid.Parse("8e445865-a24d-4543-a6c6-9443d048cdb8")
        //  }
        //);

        //    builder.Entity<IdentityUserClaim<Guid>>().HasData(
        //           new List<IdentityUserClaim<Guid>>()
        //           {
        //           new IdentityUserClaim<Guid>()
        //           {
        //               Id = 3000,
        //               ClaimType = JwtClaimTypes.Name,
        //               ClaimValue = "admin2",
        //               UserId = Guid.Parse("8e445865-a24d-4543-a6c6-9443d048cdb8")
        //           },
        //           new IdentityUserClaim<Guid>()
        //           {
        //               Id = 3001,
        //               ClaimType = JwtClaimTypes.Id,
        //               ClaimValue = "8e445865-a24d-4543-a6c6-9443d048cdb8",
        //               UserId = Guid.Parse("8e445865-a24d-4543-a6c6-9443d048cdb8")
        //           },
        //           new IdentityUserClaim<Guid>()
        //           {
        //               Id = 3002,
        //               ClaimType = ClaimTypes.Email,
        //               ClaimValue = "admin2@gmail.com",
        //               UserId = Guid.Parse("8e445865-a24d-4543-a6c6-9443d048cdb8")
        //           },
        //           new IdentityUserClaim<Guid>()
        //           {

        //               Id = 3003,
        //               ClaimType = ClaimTypes.GivenName,
        //               ClaimValue = "admin2 admin",
        //               UserId = Guid.Parse("8e445865-a24d-4543-a6c6-9443d048cdb8")
        //           },
        //            new IdentityUserClaim<Guid>()
        //           {
        //               Id = 3006,
        //               ClaimType = JwtClaimTypes.Role,
        //               ClaimValue = RoleConstants.AdminRole,
        //               UserId = Guid.Parse("8e445865-a24d-4543-a6c6-9443d048cdb8")
        //           },
        //           }
        //           );
        }
    }
}