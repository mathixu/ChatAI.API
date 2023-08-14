using AutoMapper;
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
        CreateMap<Message, MessageResponse>();


        CreateMap<ChatSession, ChatSessionResponse>().ForMember(x => x.Model,
            opt => opt.MapFrom(src => src.Model.ToString()));
        CreateMap<ChatSession, ChatSessionCreatedResponse>().ForMember(x => x.Model,
            opt => opt.MapFrom(src => src.Model.ToString()));
        CreateMap<ChatSession, GetAllChatSessionsResponse>().ForMember(x => x.Model,
            opt => opt.MapFrom(src => src.Model.ToString()));
    }
}
