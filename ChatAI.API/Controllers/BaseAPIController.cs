using AutoMapper;
using ChatAI.API.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ChatAI.API.Controllers;

[Route("[controller]")]
[ApiController]
[ApiExceptionFilter]
public abstract class BaseAPIController : ControllerBase
{
    protected readonly IMapper _mapper;

    public BaseAPIController(IMapper mapper)
    {
        _mapper = mapper;
    }

    protected Guid UserId
    {
        get
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null || !Guid.TryParse(userId, out var userIdGuid))
            {
                throw new UnauthorizedAccessException();
            }

            return userIdGuid;
        }
    }
}
