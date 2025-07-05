using MediatR;
using Microsoft.AspNetCore.Mvc;
using Wallet.API.Models;
using Wallet.Application.UseCases.Client.Common;
using Wallet.Application.UseCases.Client.CreateClient;
using Wallet.Application.UseCases.Client.GetByIdClient;

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
}
