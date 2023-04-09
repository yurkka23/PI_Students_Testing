using EduHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduHub.Persistence.DataContext.Configurations;

public class TestConfiguration : IEntityTypeConfiguration<Test>
{
    public void Configure(EntityTypeBuilder<Test> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasMany(ur => ur.TestResults)
            .WithOne(u => u.Test)
            .HasForeignKey(ur => ur.TestId)
            .IsRequired(false)
            .IsRequired();

        builder.HasMany(x => x.Questions)
           .WithOne(x => x.Test)
           .HasForeignKey(x => x.TestId)
            .IsRequired(false)
           .OnDelete(DeleteBehavior.NoAction);
    }
}

