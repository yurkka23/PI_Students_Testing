using EduHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace EduHub.Persistence.DataContext;

public class ApplicationDbContext : DbContext
{
    public DbSet<Question> Questions { get; set; }
    public DbSet<AnswerOption> AnswerOption { get; set; }

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
