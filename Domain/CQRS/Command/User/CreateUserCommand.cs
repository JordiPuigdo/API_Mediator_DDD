using Domain.Authentication;
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

namespace Domain.CQRS.Command.User
{

    public class CreateUserCommand : IRequest<CommonResponse<UserDto>>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public CreateUserCommand(NewUserRequest newUserRequest)
        {
            UserName = newUserRequest.UserName;
            Password = newUserRequest.Password;
            Email = newUserRequest.Email;
        }
    }
}
