using ChatAI.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ChatAI.Infrastructure.Persistence.Configurations;

public class ChatSessionConfiguration : IEntityTypeConfiguration<ChatSession>
{
    public void Configure(EntityTypeBuilder<ChatSession> builder)
    {
        builder.Property(e => e.Title)
            .HasMaxLength(100)
            .IsRequired(false);

        builder.HasMany(cs => cs.Messages)
            .WithOne(m => m.ChatSession)
            .HasForeignKey(m => m.ChatSessionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(cs => cs.ForkedFromMessage)
            .WithMany()
            .HasForeignKey(cs => cs.ForkedFromMessageId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(cs => cs.ForkedFromChatSession)
            .WithMany(cs => cs.ForkedChatSessions)
            .HasForeignKey(cs => cs.ForkedFromChatSessionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
