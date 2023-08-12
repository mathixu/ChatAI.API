using ChatAI.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ChatAI.Infrastructure.Persistence.Configurations;

public class MessageConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.HasOne(t => t.ChatSession)
            .WithMany(t => t.Messages)
            .HasForeignKey(t => t.ChatSessionId);
    }
}
