using MediatR;
using Microsoft.AspNetCore.Mvc;
using Wallet.API.Models;
using Wallet.Application.UseCases.Transaction.Common;
using Wallet.Application.UseCases.Transaction.CreateTransaction;
using Wallet.Application.UseCases.Transaction.GetByIdTransaction;
using Wallet.Application.UseCases.Transaction.ListTransaction;
using Wallet.Domain.Repository;

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

    [HttpGet]
    [ProducesResponseType(typeof(ApiResponseList<TransactionModelOutput>), StatusCodes.Status200OK)]
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
        var input = new ListTransactionInput();
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
        return Ok(new ApiResponseList<TransactionModelOutput>(result));
    }
}
