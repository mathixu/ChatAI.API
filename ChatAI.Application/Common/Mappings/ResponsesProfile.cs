﻿using AutoMapper;
using ChatAI.Application.Authentication.DTOs;
using ChatAI.Application.Chats.DTOs;
using ChatAI.Application.SystemPrompts.DTOs;
using ChatAI.Domain.Entities;

namespace ChatAI.Application.Common.Mappings;

public class ResponsesProfile : Profile
{
    public ResponsesProfile()
    {
        CreateMap<User, LoginResponse>();
        CreateMap<SystemPrompt, SystemPromptResponse>();
        CreateMap<ChatSession, ChatSessionResponse>();
        CreateMap<ChatSession, ChatSessionCreatedResponse>();
        CreateMap<ChatSession, GetAllChatSessionsResponse>();
        CreateMap<Message, MessageResponse>();
    }
}
