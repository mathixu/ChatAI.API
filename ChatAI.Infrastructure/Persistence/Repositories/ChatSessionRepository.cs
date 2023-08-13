using ChatAI.Application.Common.Interfaces;
using ChatAI.Domain.Entities;
using System.Linq.Expressions;

namespace ChatAI.Infrastructure.Persistence.Repositories;

public class ChatSessionRepository : BaseRepository<ChatSession>, IChatSessionRepository
{
    public ChatSessionRepository(ChatAIDbContext context) : base(context)
    {
    }

    public async Task<ChatSession?> GetDeepAsync(Expression<Func<ChatSession, bool>> expression)
    {
        var chatSession = await this.Get(expression);

        if (chatSession is null)
            return null;

        await LoadChildSessionsAndMessages(chatSession);

        return chatSession;
    }

    private async Task LoadChildSessionsAndMessages(ChatSession session)
    {
        _context.Entry(session)
            .Collection(s => s.Messages)
            .Load();

        _context.Entry(session)
            .Collection(s => s.ForkedChatSessions)
            .Load();

        foreach (var childSession in session.ForkedChatSessions)
        {
            await LoadChildSessionsAndMessages(childSession);
        }
    }
}
