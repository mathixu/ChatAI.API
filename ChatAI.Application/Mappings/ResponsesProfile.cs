using AutoMapper;
using ChatAI.Application.Authentication.DTOs;
using ChatAI.Domain.Entities;

namespace ChatAI.Application.Mappings;

public class ResponsesProfile : Profile
{
    public ResponsesProfile()
    {
        CreateMap<User, LoginResponse>();
    }
}
