﻿using ChatAI.Application.Common.DTOs;

namespace ChatAI.Application.Chats.DTOs;

public class ChatSessionResponse : BaseAuditableEntityResponse
{
    public List<MessageResponse> Messages { get; set; } = new();

    public Guid? ForkedFromMessageId { get; set; }
}