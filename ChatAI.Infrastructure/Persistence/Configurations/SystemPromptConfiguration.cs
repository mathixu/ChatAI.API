using ChatAI.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ChatAI.Infrastructure.Persistence.Configurations;

public class SystemPromptConfiguration : IEntityTypeConfiguration<SystemPrompt>
{
    public void Configure(EntityTypeBuilder<SystemPrompt> builder)
    {
        builder.Property(t => t.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(t => t.Prompt)
            .HasMaxLength(1024)
            .IsRequired();
    }
}
