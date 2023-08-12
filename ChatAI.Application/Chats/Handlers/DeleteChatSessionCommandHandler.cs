using ChatAI.Application.Chats.Commands.DeleteChatSession;
using ChatAI.Application.Common.Exceptions;
using ChatAI.Application.Common.Interfaces;
using ChatAI.Domain.Entities;
using MediatR;

namespace ChatAI.Application.Chats.Handlers;

public class DeleteChatSessionCommandHandler : IRequestHandler<DeleteChatSessionCommand>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IBaseRepository<ChatSession> _repository;

    public DeleteChatSessionCommandHandler(ICurrentUserService currentUserService, IBaseRepository<ChatSession> repository)
    {
        _currentUserService = currentUserService;
        _repository = repository;
    }

    public async Task Handle(DeleteChatSessionCommand request, CancellationToken cancellationToken)
    {
        var currentUserId = _currentUserService.GetCurrentUserId() ?? throw new UnauthorizedAccessException();

        var chatSession = await _repository.Get(cs => cs.Id == request.Id && cs.UserId == currentUserId) 
            ?? throw new NotFoundException(nameof(ChatSession), request.Id);

        
        await _repository.Delete(chatSession);
    }
}
