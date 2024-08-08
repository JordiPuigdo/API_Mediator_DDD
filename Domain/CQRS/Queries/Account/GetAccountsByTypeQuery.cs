using Domain.Abstractions.Messaging;
using Domain.ModelsDto;
using Domain.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CQRS.Queries.Account
{
    public class GetAccountsByTypeQuery : IRequest<CommonResponse<IEnumerable<AccountDto>>>, ICacheable
    {
        public string CacheKey { get; set; }
        public bool BypassCache { get; set; } = false;
        public int SlidingExpirationInMinutes { get; set; } = 30;
        public int AbsoluteExpirationInMinutes { get; set; } = 60;
        public AccountTypeDto AccountType { get; set; }

        public GetAccountsByTypeQuery(AccountTypeDto type)
        {
            CacheKey = $"Account_{type}";
            AccountType = type;
        }
    }
}
