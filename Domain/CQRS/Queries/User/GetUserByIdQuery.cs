using Domain.Abstractions.Messaging;
using Domain.ModelsDto;
using Domain.Responses;
using Domain.SharedModels;
using Domain.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CQRS.Query.User
{
    public class GetUserByIdQuery : IRequest<CommonResponse<UserDto>>, ICacheable
    {
        public string Id { get; set; }
        public string CacheKey => $"User_{Id}";
        public bool BypassCache { get; set; } = false;
        public int SlidingExpirationInMinutes { get; set; } = 30;
        public int AbsoluteExpirationInMinutes { get; set; } = 60;
        public GetUserByIdQuery(string id)
        {
            Id = id;
        }
    }
}
