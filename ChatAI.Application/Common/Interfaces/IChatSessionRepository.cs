using ChatAI.Domain.Entities;
using System.Linq.Expressions;

namespace ChatAI.Application.Common.Interfaces;

public interface IChatSessionRepository : IBaseRepository<ChatSession>
{
    Task<ChatSession?> GetDeepAsync(Expression<Func<ChatSession, bool>> expression);
}
