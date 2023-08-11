using ChatAI.API.Filters;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ChatAI.API.Controllers;

[Route("[controller]")]
[ApiController]
[ApiExceptionFilter]
public abstract class BaseAPIController : ControllerBase
{
    protected readonly IMediator _mediator;

    public BaseAPIController(IMediator mediator)
    {
        _mediator = mediator;
    }
}
