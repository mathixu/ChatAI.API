using ChatAI.API.Filters;
using ChatAI.Application.SystemPrompts.Commands.AddSystemPrompt;
using ChatAI.Application.SystemPrompts.Commands.DeleteSystemPrompt;
using ChatAI.Application.SystemPrompts.Commands.EditSystemPrompt;
using ChatAI.Application.SystemPrompts.Commands.ToggleFavoriteSystemPrompt;
using ChatAI.Application.SystemPrompts.Queries.GetAllSystemPrompts;
using ChatAI.Application.SystemPrompts.Queries.GetSystemPrompt;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatAI.API.Controllers;

[Authorize]
public class SystemPromptController : BaseAPIController
{
    public SystemPromptController(IMediator mediator) : base(mediator)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllSystemPromptsQuery());

        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var result = await _mediator.Send(new GetSystemPromptQuery(id));

        return Ok(result);
    }

    [HttpPost]
    [ValidationModelFilter(typeof(AddSystemPromptCommandValidator))]
    public async Task<IActionResult> Add([FromBody] AddSystemPromptCommand addSystemPromptCommand)
    {
        var result = await _mediator.Send(addSystemPromptCommand);

        return StatusCode(StatusCodes.Status201Created, result);
    }

    [HttpPut("{id:guid}")]
    [ValidationModelFilter(typeof(EditSystemPromptCommandValidator))]
    public async Task<IActionResult> Edit(Guid id, [FromBody] EditSystemPromptCommand editSystemPromptCommand)
    {
        editSystemPromptCommand.Id = id;

        var result = await _mediator.Send(editSystemPromptCommand);

        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteSystemPromptCommand(id));

        return NoContent();
    }

    [HttpPut("{id:guid}/toggle-favorite")]
    public async Task<IActionResult> ToggleFavorite(Guid id)
    {
        var result = await _mediator.Send(new ToggleFavoriteSystemPromptCommand(id));

        return Ok(result);
    }
}