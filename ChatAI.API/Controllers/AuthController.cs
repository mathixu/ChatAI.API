using AutoMapper;
using ChatAI.API.Filters;
using ChatAI.Application.Commands.Auth;
using ChatAI.Application.Validators;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatAI.API.Controllers;

public class AuthController : BaseAPIController
{
    public AuthController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
    {
    }

    [HttpPost("login")]
    [AllowAnonymous]
    [ValidationModelFilter(typeof(LoginCommandValidator))]
    public async Task<IActionResult> Login([FromBody] LoginCommand loginCommand)
    {
        var result = await _mediator.Send(loginCommand);

        return Ok(result);
    }


    [HttpPost("signup")]
    [AllowAnonymous]
    [ValidationModelFilter(typeof(SignUpCommandValidator))]
    public async Task<IActionResult> SignUp([FromBody] SignUpCommand signUpCommand)
    {
        return Ok();
        //return CreatedAtAction(nameof(Login), new { user.Email }, user);
    }
}
