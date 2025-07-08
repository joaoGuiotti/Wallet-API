using MediatR;
using Microsoft.AspNetCore.Mvc;
using Wallet.API.Models;
using Wallet.Application.UseCases.Account.Common;
using Wallet.Application.UseCases.Account.CreateAccount;
using Wallet.Application.UseCases.Account.CreditAccount;
using Wallet.Application.UseCases.Account.DebitAccount;
using Wallet.Application.UseCases.Account.GetByIdAccount;
using Wallet.Application.UseCases.Account.ListAccount;
using Wallet.Domain.Repository;

namespace Wallet.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{

    private readonly IMediator _mediator;

    public AccountController(IMediator mediator)
        => _mediator = mediator;

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<AccountModelOutput>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Create([FromBody] CreateAccountInput input, CancellationToken cancelationToken)
    {
        var result = await _mediator.Send(input, cancelationToken);
        return Ok(new ApiResponse<AccountModelOutput>(result));
    }

    [HttpPost("Debit")]
    [ProducesResponseType(typeof(ApiResponse<AccountModelOutput>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Debit([FromBody] DebitAccountInput input, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(input, cancellationToken);
        return Ok(new ApiResponse<AccountModelOutput>(result));
    }

    [HttpPost("Credit")]
    [ProducesResponseType(typeof(ApiResponse<AccountModelOutput>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Credit([FromBody] CreditAccountInput input, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(input, cancellationToken);
        return Ok(new ApiResponse<AccountModelOutput>(result));
    }

    [HttpGet("Balance/{account_id}")]
    [ProducesResponseType(typeof(ApiResponse<AccountModelOutput>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetBalance([FromRoute] Guid account_id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetByIdAccountInput(account_id), cancellationToken);
        return Ok(new ApiResponse<AccountModelOutput>(result));
    }

    [HttpGet]
    [ProducesResponseType(typeof(ApiResponseList<ListAccountOutput>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> List(
        [FromQuery] int? page,
        [FromQuery(Name = "per_page")] int? perPage,
        [FromQuery] string? search,
        [FromQuery] string? sort,
        [FromQuery] string? dir,
        CancellationToken cancellationToken
    )
    {
        var input = new ListAccountInput();
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
        return Ok(new ApiResponseList<AccountModelOutput>(result));
    }
}
