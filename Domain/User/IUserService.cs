using Domain.Authentication;
using Domain.Authentication.Request;
using Domain.Authentication.Response;
using Domain.ModelsDto;
using Domain.Responses;
using Domain.Responses.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.User
{
    public interface IUserService
    {
        Task<CommonResponse<UserRegisterResponse>> Register(RegisterResource resource, CancellationToken cancellationToken);
        Task<CommonResponse<LoginResponse>> Login(LoginResource resource, CancellationToken cancellationToken);
    }
}
