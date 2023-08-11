using Microsoft.EntityFrameworkCore;

namespace ChatAI.Infrastructure.Persistence;

public class ChatAIDbContextInitializer
{
    private readonly ChatAIDbContext _context;

    public ChatAIDbContextInitializer(ChatAIDbContext dbContext)
    {
        _context = dbContext;
    }

    public async Task InitializeAsync()
    {
        if (_context.Database.IsMySql())
        {
            await _context.Database.MigrateAsync();
        }
    }
}
