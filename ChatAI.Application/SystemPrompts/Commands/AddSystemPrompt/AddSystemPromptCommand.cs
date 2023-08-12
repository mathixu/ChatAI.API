using ChatAI.Application.SystemPrompts.DTOs;
using MediatR;

namespace ChatAI.Application.SystemPrompts.Commands.AddSystemPrompt;

public class AddSystemPromptCommand : IRequest<SystemPromptResponse>
{
    public string Name { get; set; } = default!;
    public string Prompt { get; set; } = default!;
}
