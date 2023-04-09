using EduHub.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace EduHub.Persistence.DataContext;

public class ApplicationDbContext : IdentityDbContext<User, AppRole, Guid>
{
    //public DbSet<User> Users { get; set; }
    public DbSet<AnswerOption> AnswerOptions { get; set; }
    public DbSet<AppRole> AppRoles { get; set; }
    public DbSet<AppUserRole> AppUserRoles { get; set; }
    public DbSet<Course> Course { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<Test> Tests { get; set; }
    public DbSet<TestResult> TestResults { get; set; }
    public DbSet<UserCourse> UserCourses { get; set; }
    public DbSet<TeacherRequest> teacherRequests { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
          : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
