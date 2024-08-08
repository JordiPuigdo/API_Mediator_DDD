using Domain.Authentication;
using Domain.CQRS.Command.User;
using Domain.ModelsDto;
using Domain.Requests.User;
using Domain.Responses;
using Domain.SharedModels;
using Domain.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.User
{
        public class CreateUserHandler : IRequestHandler<CreateUserCommand, CommonResponse<UserDto>>
        {
            private readonly IUserService _userService;

            public CreateUserHandler(IUserService userService)
            {
                _userService = userService;
            }

            public async Task<CommonResponse<UserDto>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
            {
                var user = new RegisterResource(request.UserName, request.Email, request.Password);

                var createdUser = await _userService.Register(user, cancellationToken);
                if (createdUser.Result.ResultNumber == 0)
                {
                    return new CommonResponse<UserDto>
                    {
                        Data = new UserDto
                        {
                            Id = createdUser.Data.Id,
                            UserName = createdUser.Data.UserName,
                            Password = createdUser.Data.Password,
                            PasswordHash = createdUser.Data.PasswordHash,
                            Email = createdUser.Data.Email
                        },
                        Result = createdUser.Result
                    };
                }

                return new CommonResponse<UserDto>
                {
                    Data = null,
                    Result = createdUser.Result
                };

            }
        }

}
