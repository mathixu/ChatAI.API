using ChatAI.Domain.Entities;
using ChatAI.Infrastructure.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore;

namespace ChatAI.Infrastructure.Persistence;

public class ChatAIDbContext : DbContext
{
    private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChangesInterceptor;

    public DbSet<User> Users { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<ResetPasswordToken> ResetPasswordTokens { get; set; }
    public DbSet<ChatSession> ChatSessions { get; set; }
    public DbSet<SystemPrompt> SystemPrompts { get; set; }
    public DbSet<Message> Messages { get; set; }

    public ChatAIDbContext(DbContextOptions<ChatAIDbContext> options, AuditableEntitySaveChangesInterceptor auditableEntitySaveChangesInterceptor) : base(options)
    {
        _auditableEntitySaveChangesInterceptor = auditableEntitySaveChangesInterceptor;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ChatAIDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_auditableEntitySaveChangesInterceptor);
    }
}

/* COMMANDS
 dotnet ef migrations add InitEntities -s .\ChatAI.API\  -p .\ChatAI.Infrastructure\ -o .\Persistence\Migrations\
 dotnet ef database update -s .\ChatAI.API\  -p .\ChatAI.Infrastructure\
 */