using MediatR;

namespace ChatAI.Application.Chats.Commands.DeleteChatSession;

public class DeleteChatSessionCommand : IRequest
{
    public Guid Id { get; set; }

    public DeleteChatSessionCommand(Guid id)
    {
        Id = id;
    }
}
