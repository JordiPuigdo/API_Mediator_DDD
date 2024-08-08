using Domain.Abstractions.Providers;
using Domain.CQRS.Queries.Contact;
using Domain.Interfaces.Contacts;
using Domain.ModelsDto;
using Domain.Requests.Contact;
using Domain.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Contact
{
    public class GetContactConfigurationHandler : IRequestHandler<GetContactConfigurationQuery, CommonResponse<IEnumerable<ContactSettingsDto>>>
    {
        private readonly IContactsRepository _contactsRepository;
        public GetContactConfigurationHandler(IContactsRepository contactsRepository)
        {
            _contactsRepository = contactsRepository;
        }
        public async Task<CommonResponse<IEnumerable<ContactSettingsDto>>> Handle(GetContactConfigurationQuery request, CancellationToken cancellationToken)
        {
            var requestConfig = new GetContactConfigurationRequest
            {
                ContactSettingsType = request.ContactSettingType,
                ContactId = request.ContactId,
            };
            var result = await _contactsRepository.GetContactsConfigurations(requestConfig);
            if(result != null)
            {
                return new CommonResponse<IEnumerable<ContactSettingsDto>>
                {
                    Data = result,
                    Result = new Result { ErrorMessage = string.Empty, ResultNumber = 0 }
                };
            }
            return new CommonResponse<IEnumerable<ContactSettingsDto>>
            {
                Data = null,
                Result = new Result { ErrorMessage = "No elements", ResultNumber = 1 }
            };
        }
    }
}
