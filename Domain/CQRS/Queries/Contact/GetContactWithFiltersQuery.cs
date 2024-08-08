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
    public class GetContactWithFiltersQuery : IRequest<CommonResponse<IEnumerable<ContactDto>>>, ICacheable
    {
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Name { get; set; }
        public string ContactId { get; set; }   
        public ContactTypeDto ContactType { get; set; }

        public GetContactWithFiltersQuery(GetContactWithFiltersRequest request)
        {
            Email = request.Email;
            Phone = request.Phone;
            Name = request.Name;
            ContactId = request.ContactId;
            ContactType = request.Type;
        }

        public string CacheKey => $"ContactWithFilters_{Name}_{Phone}_{Email}_{ContactType}";
        public bool BypassCache { get; set; } = false;
        public int SlidingExpirationInMinutes { get; set; } = 30;
        public int AbsoluteExpirationInMinutes { get; set; } = 60;
    }

}
