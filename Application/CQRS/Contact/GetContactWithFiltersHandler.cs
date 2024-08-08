using Domain.Abstractions.Providers;
using Domain.CQRS.Command.Contact;
using Domain.CQRS.Queries.Contact;
using Domain.CQRS.Query.User;
using Domain.Interfaces.Contacts;
using Domain.ModelsDto;
using Domain.Requests.Contact;
using Domain.Responses;
using Domain.SharedModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Contact
{
    public class GetContactWithFiltersHandler : IRequestHandler<GetContactWithFiltersQuery, CommonResponse<IEnumerable<ContactDto>>>
    {
        private readonly IContactsRepository _contacts;

        public GetContactWithFiltersHandler(IContactsRepository contacts)
        {
            _contacts = contacts;
        }

        public async Task<CommonResponse<IEnumerable<ContactDto>>> Handle(GetContactWithFiltersQuery request, CancellationToken cancellationToken)
        {
            var contacts = await _contacts.GetByFilters(new GetContactWithFiltersRequest
            {
                Name = request.Name,
                Email = request.Email,
                Phone = request.Phone,
                ContactId = request.ContactId
            });

            return new CommonResponse<IEnumerable<ContactDto>>
            {
                Data = contacts,
                Result = new Result { ErrorMessage = string.Empty, ResultNumber = 0 }
            };
        }
    }

}
