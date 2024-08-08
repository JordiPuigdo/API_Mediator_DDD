using Database.Models;
using Domain.Authentication;
using Domain.Authentication.Request;
using Domain.CQRS.Command.User;
using Domain.CQRS.Query.User;
using Domain.ModelsDto;
using Domain.Requests.User;
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

    public class UserController : ControllerBase
    {
        private readonly ICommonService _commonService;
        private readonly IUserService _userService;
        private readonly IMediator _mediator;

        public UserController(ICommonService commonService, IUserService userService, IMediator mediator)
        {
            _commonService = commonService;
            _userService = userService; 
            _mediator = mediator;
        }


        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<CommonResponse<User>>> GetUserById(string id)
        {
            var query = new GetUserByIdQuery(id);
            var response = await _mediator.Send(query);
            if (response.Result.ResultNumber != 0)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("All")]
        
        public async Task<ActionResult<CommonResponse<IEnumerable<UserDto>>>> GetAllUsers()
        {
            var query = new GetAllUsersQuery();
            var response = await _mediator.Send(query);
            if (response.Result.ResultNumber != 0)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost("Register")]
        public async Task<ActionResult<CommonResponse<UserDto>>> Register(RegisterResource resource, CancellationToken cancellationToken)
        {
            var response = await _userService.Register(resource, cancellationToken);
            if (response.Result.ResultNumber != 0)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginResource resource, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _userService.Login(resource, cancellationToken);
                if (response.Result.ResultNumber != 0)
                {
                    return BadRequest(response);
                }
                return Ok(response);
            }
            catch (Exception e)
            {
                return BadRequest(new { ErrorMessage = e.Message });
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<CommonResponse<UserDto>>> CreateUser(NewUserRequest newUserRequest)
        {
            var command = new CreateUserCommand(newUserRequest);
            var response = await _mediator.Send(command);

            if (response.Result.ResultNumber != 0)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPut]
        [Authorize]

        public async Task<ActionResult<CommonResponse<User>>> UpdateUser(UserDto user)
        {
            var response = await _commonService.Update(user);
            if (response.Result.ResultNumber != 0)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<CommonResponse<bool>>> DeleteUser(string id)
        {
            
            var response = await _commonService.Delete<User>(id);
            if (response.Result.ResultNumber != 0)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
