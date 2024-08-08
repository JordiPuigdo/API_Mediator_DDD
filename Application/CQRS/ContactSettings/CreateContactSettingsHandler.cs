using Domain.Abstractions;
using Domain.CQRS.Command.Contact;
using Domain.CQRS.Command.ContactSettings;
using Domain.Interfaces.Contacts;
using Domain.ModelsDto;
using Domain.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.ContactSettings
{
    public class CreateContactSettingsHandler : IRequestHandler<CreateContactSettingsCommand, CommonResponse<ContactSettingsDto>>
    {
        private readonly IContactsRepository _contactsRepository;
        private readonly IMediator _mediator;

        public CreateContactSettingsHandler(IContactsRepository contactRepository, IMediator mediator) 
        {
            _contactsRepository = contactRepository;
            _mediator = mediator;
        }

        public async Task<CommonResponse<ContactSettingsDto>> Handle(CreateContactSettingsCommand request, CancellationToken cancellationToken)
        {
            var key = $"GetContactConfigurationQuery_{request.ContactSettingsType}_{request.ContactId}";
            await _mediator.Send(new RemoveCacheEntryCommand(key), cancellationToken);

            var contactSettings = new ContactSettingsDto()
            {
                Code = request.Code,
                Description = request.Description,
                IsDefault = false,
                ContactSettingsType = request.ContactSettingsType,
            };

            var response = await _contactsRepository.AddSettings(contactSettings, request.ContactId);

            if (response == null)
            {
                return new CommonResponse<ContactSettingsDto>
                {
                    Data = null,
                    Result = new Result { ErrorMessage = "Error creating settings", ResultNumber = 1 },
                };
            }

            return new CommonResponse<ContactSettingsDto>
            {
                Data = response,
                Result = new Result { ErrorMessage = string.Empty, ResultNumber = 0 },
            };
        }
    }
}
