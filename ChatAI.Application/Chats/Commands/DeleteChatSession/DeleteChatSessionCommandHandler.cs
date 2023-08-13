using ChatAI.Application.Common.Exceptions;
using ChatAI.Application.Common.Interfaces;
using ChatAI.Domain.Entities;
using MediatR;

namespace ChatAI.Application.Chats.Commands.DeleteChatSession;

public class DeleteChatSessionCommandHandler : IRequestHandler<DeleteChatSessionCommand>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IChatSessionRepository _repository;

    public DeleteChatSessionCommandHandler(ICurrentUserService currentUserService, IChatSessionRepository repository)
    {
        _currentUserService = currentUserService;
        _repository = repository;
    }

    public async Task Handle(DeleteChatSessionCommand request, CancellationToken cancellationToken)
    {
        var currentUserId = _currentUserService.UserId ?? throw new UnauthorizedAccessException();

        var chatSession = await _repository.Get(cs => cs.Id == request.Id && cs.UserId == currentUserId && cs.ForkedFromMessageId == null)
            ?? throw new NotFoundException(nameof(ChatSession), request.Id);


        await _repository.Delete(chatSession);
    }
}
