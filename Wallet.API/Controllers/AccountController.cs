using MediatR;
using Microsoft.AspNetCore.Mvc;
using Wallet.API.Models;
using Wallet.Application.UseCases.Account.Common;
using Wallet.Application.UseCases.Account.CreateAccount;
using Wallet.Application.UseCases.Account.CreditAccount;
using Wallet.Application.UseCases.Account.DebitAccount;
using Wallet.Application.UseCases.Account.GetByIdAccount;

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

    [HttpGet("Balances/{account_id}")]
    [ProducesResponseType(typeof(ApiResponse<AccountModelOutput>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetBalance([FromRoute] Guid account_id, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetByIdAccountInput(account_id), cancellationToken);
        return Ok(new ApiResponse<AccountModelOutput>(result));
    }
}
