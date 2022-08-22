using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Web.Http;
using SimpleApi.Model.Logic;
using SimpleApi.Model.Tasks.Logic;

namespace SimpleApi.Web.Controllers;

[ApiController]
[ApiVersion("1")]
[Route("api/v{version:apiVersion}/tasks")]
public class TaskController : Controller
{
    private IMediator _mediator;
    private ILogger<TaskController> _logger;

    public TaskController(IMediator mediator, ILogger<TaskController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(GetTaskByIdResponse), (int) HttpStatusCode.OK)]
    public async Task<IActionResult> Get([FromRoute] Guid id)
    {
        try
        {
            var result = await _mediator.Send(new GetTaskByIdRequest(id));
            return Ok(result);
        }
        catch (NullReferenceException e)
        {
            _logger.LogError("Item with id: {id} is not found", id);
            return NotFound();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occured during GET task action");
            throw;
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _mediator.Send(new GetAllTasksRequest());
        if (result.Items is null || result.Items.Any() == false)
            return NotFound();

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateTaskRequest request)
    {
        var result = await _mediator.Send(request);
        return CreatedAtAction(nameof(Post), result);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] UpdateDto request)
    {
        var result =
            await _mediator.Send(new UpdateTaskRequest(id, request.Title, request.DueDate, request.Desctiption));
        return Ok(result);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute]Guid id)
    {
        try
        {
            var result = await _mediator.Send(new DeleteTaskRequest(id));
            return NoContent();
        }
        catch (NullReferenceException e)
        {
            _logger.LogError("Item with id: {id} is not found", id);
            return NotFound();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occured during GET task action");
            throw;
        }
    }
}

public record UpdateDto(string Title, string Desctiption, DateTime DueDate);