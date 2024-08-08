using Microsoft.AspNetCore.Mvc;

namespace HubmaSoftAPI.Controllers
{
    using Application.CQRS.Contact;
    using Database.Models;
    using Domain.Abstractions;
    using Domain.Authentication;
    using Domain.Authentication.Request;
    using Domain.CQRS.Command.Contact;
    using Domain.CQRS.Command.ContactSettings;
    using Domain.CQRS.Command.User;
    using Domain.CQRS.Queries.Contact;
    using Domain.CQRS.Query.User;
    using Domain.ModelsDto;
    using Domain.Requests.Contact;
    using Domain.Requests.ContactSettings;
    using Domain.Requests.User;
    using Domain.Responses;
    using Domain.SharedModels;
    using Domain.User;
    using MediatR;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading;

    namespace HubmaSoftAPI.Controllers
    {
        [ApiController]
        [Route("[controller]")]

        public class ContactController : ControllerBase
        {
            private readonly IMediator _mediator;

            public ContactController(ICommonService commonService, IUserService userService, IMediator mediator)
            {
                _mediator = mediator;
            }


            [HttpPost]
            [Authorize]
            public async Task<ActionResult<CommonResponse<AccountDto>>> CreateContact(NewContactRequest request)
            {
                var command = new CreateContactCommand(request);
                var response = await _mediator.Send(command);

                if (response.Result.ResultNumber != 0)
                {
                    return BadRequest(response);
                }

                return Ok(response);
            }

            [HttpGet("{id}")]
            [Authorize]
            public async Task<ActionResult<CommonResponse<IEnumerable<AccountDto>>>> GetById(string id)
            {
                var query = new GetAllContactsQuery(id);
                var response = await _mediator.Send(query);
                if (response.Result.ResultNumber != 0)
                {
                    return BadRequest(response);
                }
                return Ok(response);
            }

            [HttpGet("All")]
            [Authorize]
            public async Task<ActionResult<CommonResponse<IEnumerable<AccountDto>>>> GetAll()
            {
                var query = new GetAllContactsQuery(string.Empty);
                var response = await _mediator.Send(query);
                if (response.Result.ResultNumber != 0)
                {
                    return BadRequest(response);
                }
                return Ok(response);
            }

            [HttpGet("WithFilters")]
            [Authorize]
            public async Task<ActionResult<CommonResponse<IEnumerable<AccountDto>>>> GetWithFilters(GetContactWithFiltersRequest request)
            {
                var query = new GetContactWithFiltersQuery(request);
                var response = await _mediator.Send(query);
                if (response.Result.ResultNumber != 0)
                {
                    return BadRequest(response);
                }
                return Ok(response);
            }

            [HttpPut]
            [Authorize]
            public async Task<ActionResult<CommonResponse<AccountDto>>> UpdateContact(UpdateContactRequest request)
            {
                var command = new UpdateContactCommand(request);
                var response = await _mediator.Send(command);

                if (response.Result.ResultNumber != 0)
                {
                    return BadRequest(response);
                }

                return Ok(response);
            }


            [HttpGet("ContactConfiguration")]

            public async Task<ActionResult<CommonResponse<IEnumerable<ContactConfigurationDto>>>> ContactConfiguration(GetContactConfigurationRequest request)
            {
                var query = new GetContactConfigurationQuery(request);
                var response = await _mediator.Send(query);
                if (response.Result.ResultNumber != 0)
                {
                    return BadRequest(response);
                }
                return Ok(response);
            }

            [HttpPost("CreateContactSettings")]
            public async Task<ActionResult<CommonResponse<IEnumerable<ContactSettingsDto>>>> CreateContactSettings(CreateContactSettingsRequest request)
            {
                var query = new CreateContactSettingsCommand(request);
                var response = await _mediator.Send(query);
                if (response.Result.ResultNumber != 0)
                {
                    return BadRequest(response);
                }
                return Ok(response);
            }


            [HttpDelete("DeleteContactSettings")]
            public async Task<ActionResult<CommonResponse<IEnumerable<ContactSettingsDto>>>> DeleteContactSettings(string settingsId, string contactId)
            {
                var query = new DeleteContactSettingsCommand(contactId, settingsId);
                var response = await _mediator.Send(query);
                if (response.Result.ResultNumber != 0)
                {
                    return BadRequest(response);
                }
                return Ok(response);
            }
        }


        
    }

}
