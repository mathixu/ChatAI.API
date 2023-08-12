using ChatAI.Application.SystemPrompts.DTOs;
using MediatR;

namespace ChatAI.Application.SystemPrompts.Queries.GetSystemPrompt;

public class GetSystemPromptQuery : IRequest<SystemPromptResponse>
{
    public Guid Id { get; set; }

    public GetSystemPromptQuery(Guid id)
    {
        Id = id;
    }
}
