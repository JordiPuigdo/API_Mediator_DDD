using Domain.Abstractions.Messaging;
using Domain.ModelsDto;
using Domain.Responses;
using Domain.SharedModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CQRS.Query.User
{
    public class GetAllUsersQuery : IRequest<CommonResponse<IEnumerable<UserDto>>>, ICacheable
    {
        public string CacheKey => "UsersAll";
        public bool BypassCache { get; set; } = false;
        public int SlidingExpirationInMinutes { get; set; } = 30;
        public int AbsoluteExpirationInMinutes { get; set; } = 60;
    }


}
