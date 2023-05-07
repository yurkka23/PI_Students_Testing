using EduHub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EduHub.Persistence.DataContext.Configurations
{
    public class QuestionConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasMany(ur => ur.Answers)
                .WithOne(u => u.Question)
                .HasForeignKey(ur => ur.QuestionId)
                .IsRequired(false)
                .IsRequired();
        }
    }
}