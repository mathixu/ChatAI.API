using ChatAI.API.Filters;
using ChatAI.Application.Chats.Commands.AddChatSession;
using ChatAI.Application.Chats.Commands.AddMessage;
using ChatAI.Application.Chats.Commands.DeleteChatSession;
using ChatAI.Application.Chats.Commands.EditChatSessionTitle;
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
    [ValidationModelFilter(typeof(AddChatSessionCommandValidator))]
    public async Task<IActionResult> Create([FromBody] AddChatSessionCommand addChatSessionCommand)
    {
        var result = await _mediator.Send(addChatSessionCommand);

        return Ok(result);
    }

    [HttpPut("{id:guid}/title")]
    [ValidationModelFilter(typeof(EditChatSessionTitleCommandValidator))]
    public async Task<IActionResult> Edit(Guid id, [FromBody] EditChatSessionTitleCommand editChatSessionTitleCommand)
    {
        var result = await _mediator.Send(new EditChatSessionTitleCommand(id, editChatSessionTitleCommand));

        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteChatSessionCommand(id));

        return NoContent();
    }



    [HttpPost("{id:guid}/messages")]
    [ValidationModelFilter(typeof(AddMessageCommandValidator))]
    public async Task<IActionResult> AddMessage(Guid id, [FromBody] AddMessageCommand addMessageCommand)
    {
        var result = await _mediator.Send(new AddMessageCommand(id, addMessageCommand));

        return Ok(result);
    }
}
