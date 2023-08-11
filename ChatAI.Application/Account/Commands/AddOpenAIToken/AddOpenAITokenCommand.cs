using MediatR;

namespace ChatAI.Application.Account.Commands.AddOpenAIToken;

public class AddOpenAITokenCommand : IRequest
{
    public string OpenAIToken { get; set; } = default!;
}
