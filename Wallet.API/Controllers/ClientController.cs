using MediatR;
using Microsoft.AspNetCore.Mvc;
using Wallet.API.Models;
using Wallet.Application.UseCases.Client.Common;
using Wallet.Application.UseCases.Client.CreateClient;
using Wallet.Application.UseCases.Client.GetByIdClient;
using Wallet.Application.UseCases.Client.ListClient;
using Wallet.Domain.Repository;

namespace Wallet.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ClientController : ControllerBase
{
    private readonly IMediator _mediator;

    public ClientController(IMediator mediator)
        => _mediator = mediator;

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<ClientModelOutput>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Create([FromBody] CreateClientInput command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(new ApiResponse<ClientModelOutput>(result));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<ClientModelOutput>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetByIdClientInput(id), cancellationToken);
        return Ok(new ApiResponse<ClientModelOutput>(result));
    }

    [HttpGet]
    [ProducesResponseType(typeof(ApiResponseList<ListClientOutput>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> List(
        [FromQuery] int? page,
        [FromQuery(Name = "per_page")] int? perPage,
        [FromQuery] string? search,
        [FromQuery] string? sort,
        [FromQuery] string? dir,
        CancellationToken cancellationToken
    )
    { 
        var input = new ListClientInput();
        if (page.HasValue)
            input.Page = page.Value;
        if (perPage.HasValue)
            input.PerPage = perPage.Value;
        if (!string.IsNullOrEmpty(search))
            input.SearchTerm = search;
        if (!string.IsNullOrEmpty(sort))
            input.Sort = sort;
        if (!string.IsNullOrEmpty(dir))
            input.Dir = Enum.TryParse<ESearchOrder>(dir, true, out var order) ? order : ESearchOrder.Asc;
        var result = await _mediator.Send(input, cancellationToken);
        return Ok(new ApiResponseList<ClientModelOutput>(result));
    }

}
