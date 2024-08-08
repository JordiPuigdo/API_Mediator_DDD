using Domain.CQRS.Command.Account;
using Domain.CQRS.Queries.Account;
using Domain.CQRS.Queries.Contact;
using Domain.ModelsDto;
using Domain.Requests.Account;
using Domain.Requests.Contact;
using Domain.Responses;
using Domain.SharedModels;
using Domain.User;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HubmaSoftAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        //[Authorize]
        public async Task<ActionResult<CommonResponse<IEnumerable<AccountDto>>>> Get(AccountTypeDto type)
        {
            var query = new GetAccountsByTypeQuery(type);
            var response = await _mediator.Send(query);
            if (response.Result.ResultNumber != 0)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost]
        //[Authorize]
        public async Task<ActionResult<CommonResponse<AccountDto>>> Create(NewAccountRequest request)
        {
            var query = new CreateAccountCommand(request);
            var response = await _mediator.Send(query);
            if (response.Result.ResultNumber != 0)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpDelete]
        //[Authorize]
        public async Task<ActionResult<CommonResponse<AccountDto>>> Delete(string id)
        {
            var query = new DeleteAccountCommand(id);
            var response = await _mediator.Send(query);
            if (response.Result.ResultNumber != 0)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPut]
        //[Authorize]
        public async Task<ActionResult<CommonResponse<AccountDto>>> Update(UpdateAccountRequest updateAccountRequest)
        {
            var query = new UpdateAccountCommand(updateAccountRequest);
            var response = await _mediator.Send(query);
            if (response.Result.ResultNumber != 0)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
