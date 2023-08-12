﻿using ChatAI.Application.Common.DTOs;

namespace ChatAI.Application.Chats.DTOs;

public class ChatSessionCreatedResponse : BaseAuditableEntityResponse
{
    public string? Title { get; set; }
}
