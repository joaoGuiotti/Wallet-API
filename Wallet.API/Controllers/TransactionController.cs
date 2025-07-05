using MediatR;
using Microsoft.AspNetCore.Mvc;
using Wallet.API.Models;
using Wallet.Application.UseCases.Transaction.Common;
using Wallet.Application.UseCases.Transaction.CreateTransaction;
using Wallet.Application.UseCases.Transaction.GetByIdTransaction;

namespace Wallet.API.Controllers;

[Route("[controller]")]
[ApiController]
public class TransactionController : ControllerBase
{

    private readonly IMediator _mediator;

    public TransactionController(IMediator mediator)
        => _mediator = mediator;

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<TransactionModelOutput>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Create([FromBody] CreateTransactionInput input, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(input, cancellationToken);
        return Ok(new ApiResponse<TransactionModelOutput>(result));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<TransactionModelOutput>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetByIdTransactionInput(id), cancellationToken);
        return Ok(new ApiResponse<TransactionModelOutput>(result));
    }

}
