using ChatAI.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ChatAI.Infrastructure.Persistence.Configurations;

public class ChatSessionConfiguration : IEntityTypeConfiguration<ChatSession>
{
    public void Configure(EntityTypeBuilder<ChatSession> builder)
    {
        builder.HasMany(cs => cs.Messages)
            .WithOne(m => m.ChatSession)
            .HasForeignKey(m => m.ChatSessionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(cs => cs.ForkedFromMessage)
            .WithMany()
            .HasForeignKey(cs => cs.ForkedFromMessageId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
