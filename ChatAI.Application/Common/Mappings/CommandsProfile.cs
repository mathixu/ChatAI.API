using AutoMapper;
using ChatAI.Application.Authentication.Commands.SignUp;
using ChatAI.Application.Chats.Commands.AddChatSession;
using ChatAI.Application.Chats.Commands.AddMessage;
using ChatAI.Application.SystemPrompts.Commands.AddSystemPrompt;
using ChatAI.Application.SystemPrompts.Commands.EditSystemPrompt;
using ChatAI.Domain.Entities;

namespace ChatAI.Application.Common.Mappings;

public class CommandsProfile : Profile
{
    public CommandsProfile()
    {
        CreateMap<SignUpCommand, User>();
        CreateMap<AddSystemPromptCommand, SystemPrompt>();

        CreateMap<EditSystemPromptCommand, SystemPrompt>().ForMember(x => x.Id, opt => opt.Ignore());
        
        CreateMap<AddMessageCommand, Message>();
        CreateMap<AddChatSessionCommand, ChatSession>();
    }
}
