using MediatR;

namespace ChatAI.Application.SystemPrompts.Commands.DeleteSystemPrompt;

public class DeleteSystemPromptCommand : IRequest
{
    public Guid Id { get; set; }

    public DeleteSystemPromptCommand(Guid id)
    {
        Id = id;
    }
}
