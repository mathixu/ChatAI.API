using AutoMapper;
using ChatAI.Application.Authentication.Commands.SignUp;
using ChatAI.Domain.Entities;

namespace ChatAI.Application.Mappings;

public class CommandsProfile : Profile
{
    public CommandsProfile()
    {
        CreateMap<SignUpCommand, User>();
    }
}
