using Domain.Abstractions.Messaging;
using Domain.ModelsDto;
using Domain.Requests.Contact;
using Domain.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CQRS.Queries.Contact
{
    public class GetAllContactsQuery : IRequest<CommonResponse<IEnumerable<AccountDto>>>, ICacheable
    {
        public string CacheKey { get; set; }
        public bool BypassCache { get; set; } = false;
        public int SlidingExpirationInMinutes { get; set; } = 30;
        public int AbsoluteExpirationInMinutes { get; set; } = 60;
        public string ContactId { get; set; } = string.Empty;
        public GetAllContactsQuery(string id)
        {
            CacheKey = String.IsNullOrEmpty(id) ? "ContactsAll" : "ConctactId_" + id;
            ContactId = id ?? string.Empty;
        }
    }
}
