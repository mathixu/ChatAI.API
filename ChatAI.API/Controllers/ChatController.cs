using ChatAI.Application.Chats.Commands.AddChatSession;
using ChatAI.Application.Chats.Commands.DeleteChatSession;
using ChatAI.Application.Chats.Queries.GetAllChats;
using ChatAI.Application.Chats.Queries.GetChat;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatAI.API.Controllers;

[Authorize]
public class ChatController : BaseAPIController
{
    public ChatController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllChatsQuery());

        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var result = await _mediator.Send(new GetChatQuery(id));

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create()
    {
        var result = await _mediator.Send(new AddChatSessionCommand());

        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteChatSessionCommand(id));

        return NoContent();
    }
}
