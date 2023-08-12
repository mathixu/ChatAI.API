using MediatR;

namespace ChatAI.Application.Accounts.Commands.AddOpenAIToken;

public class AddOpenAITokenCommand : IRequest
{
    public string OpenAIToken { get; set; } = default!;
}
