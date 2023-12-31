﻿using ChatAI.API.Filters;
using ChatAI.Application.Accounts.Commands.AddOpenAIToken;
using ChatAI.Application.Accounts.Commands.DeleteMyAccount;
using ChatAI.Application.Accounts.Commands.DeleteOpenAIToken;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatAI.API.Controllers;

[Authorize]
public class AccountController : BaseAPIController
{
    public AccountController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPut("openai-token")]
    [ValidationModelFilter(typeof(AddOpenAITokenCommandValidator))]
    public async Task<IActionResult> AddOpenAIToken([FromBody] AddOpenAITokenCommand addOpenAITokenCommand)
    {
        await _mediator.Send(addOpenAITokenCommand);

        return Ok(new { Message = "OpenAI Token updated successfully" });
    }

    [HttpDelete("openai-token")]
    public async Task<IActionResult> DeleteOpenAIToken()
    {
        await _mediator.Send(new DeleteOpenAITokenCommand());

        return NoContent();
    }

    [HttpDelete("me")]
    [ValidationModelFilter(typeof(DeleteMyAccountCommandValidator))]
    public async Task<IActionResult> DeleteMyAccount([FromBody] DeleteMyAccountCommand deleteMyAccountCommand)
    {
        await _mediator.Send(deleteMyAccountCommand);

        return NoContent();
    }
}