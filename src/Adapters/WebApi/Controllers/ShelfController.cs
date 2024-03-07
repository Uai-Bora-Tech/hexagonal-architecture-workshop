using Application.Contracts.ShelfService;
using Application.Ports.Handles;
using Domain.Aggregates;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class ShelfController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateShelfAsync([FromServices] ICommandHandler<Request.CreateShelf> handler, [FromBody] Request.CreateShelf request, CancellationToken cancellationToken)
    {
		try
		{
			await handler.Handle(request, cancellationToken);

			return Ok();
		}
		catch (Exception ex)
		{
			return BadRequest(ex);
		}
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetShelfAsync([FromServices] IQueryHandler<Request.GetShelf, Shelf> handler, [FromRoute] Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var shelf = await handler.Handle(new Request.GetShelf(id), cancellationToken);

            return Ok(shelf);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }
}
