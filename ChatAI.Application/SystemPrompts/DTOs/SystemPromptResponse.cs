using ChatAI.Application.Common.DTOs;

namespace ChatAI.Application.SystemPrompts.DTOs;

public class SystemPromptResponse : BaseAuditableEntityResponse
{
    public string Name { get; set; } = default!;
    public string Prompt { get; set; } = default!;
    public bool IsFavorite { get; set; } = false;
}
