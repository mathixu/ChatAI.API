using ChatAI.Application.SystemPrompts.DTOs;
using MediatR;
using System.Text.Json.Serialization;

namespace ChatAI.Application.SystemPrompts.Commands.EditSystemPrompt;

public class EditSystemPromptCommand : IRequest<SystemPromptResponse>
{
    public string Name { get; set; } = default!;
    public string Prompt { get; set; } = default!;

    [JsonIgnore]
    public Guid Id { get; set; }

    public EditSystemPromptCommand(Guid id, EditSystemPromptCommand editSystemPromptCommand)
    {
        Id = id;
        Name = editSystemPromptCommand.Name;
        Prompt = editSystemPromptCommand.Prompt;
    }

}
