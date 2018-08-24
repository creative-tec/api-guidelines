namespace CT.ApiGuidelines.Api.Resources.Accounts
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Mediator.Accounts;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Models.Accounts;

    [Route("accounts")]
    public class AccountsController : Controller
    {
        private const string GetAccountActionName = "GetAccount";
        private readonly IMediator _mediator;

        public AccountsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountGetV1>>> GetAll()
        {
            var accounts = await _mediator.Send(new GetAllQuery()).ConfigureAwait(false);

            return Ok(accounts);
        }

        [HttpGet("{id}", Name = GetAccountActionName)]
        public async Task<ActionResult<AccountGetV1>> GetAccount(Guid id)
        {
            var account = await _mediator.Send(new GetAccountByIdQuery(id)).ConfigureAwait(false);

            if (account.HasValue)
            {
                return Ok(account.Value);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<AccountGetV1>> AddAccount([FromBody] AccountPostV1 accountPost)
        {
            if (accountPost == null)
            {
                return BadRequest();
            }

            var account = await _mediator.Send(new AddAccountCommand(accountPost)).ConfigureAwait(false);

            if (account.IsSuccess)
            {
                return CreatedAtAction(GetAccountActionName, new { id = account.Value.Id }, account.Value);
            }

            var modelState = new ModelStateDictionary();

            modelState.AddModelError("Failure", account.Error);

            return UnprocessableEntity(modelState);
        }
    }
}
