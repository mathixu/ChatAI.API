﻿using MediatR;
using ChatAI.Domain.Entities;
using ChatAI.Application.Common.Interfaces;

namespace ChatAI.Application.Accounts.Commands.AddOpenAIToken;

public class AddOpenAITokenCommandHandler : IRequestHandler<AddOpenAITokenCommand>
{
    private readonly IBaseRepository<User> _userRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IEncryptionService _encryptionService;

    public AddOpenAITokenCommandHandler(IBaseRepository<User> userRepository, ICurrentUserService currentUserService, IEncryptionService encryptionService)
    {
        _userRepository = userRepository;
        _currentUserService = currentUserService;
        _encryptionService = encryptionService;
    }

    public async Task Handle(AddOpenAITokenCommand request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId ?? throw new UnauthorizedAccessException();

        var user = await _userRepository.Get(userId) ?? throw new UnauthorizedAccessException();

        user.OpenAIToken = _encryptionService.Encrypt(request.OpenAIToken);

        await _userRepository.Update(user);
    }
}