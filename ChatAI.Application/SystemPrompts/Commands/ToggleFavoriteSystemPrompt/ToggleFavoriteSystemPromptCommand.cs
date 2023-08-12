using ChatAI.Application.SystemPrompts.DTOs;
using MediatR;

namespace ChatAI.Application.SystemPrompts.Commands.ToggleFavoriteSystemPrompt;

public class ToggleFavoriteSystemPromptCommand : IRequest<SystemPromptResponse>
{
    public Guid Id { get; set; }

    public ToggleFavoriteSystemPromptCommand(Guid id)
    {
        Id = id;
    }
}
