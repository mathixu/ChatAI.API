using ChatAI.Application.SystemPrompts.DTOs;
using MediatR;

namespace ChatAI.Application.SystemPrompts.Queries.GetAllSystemPrompts;

public class GetAllSystemPromptsQuery : IRequest<List<SystemPromptResponse>>
{
}
