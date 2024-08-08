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
    public class DeleteContactSettingsHandler : IRequestHandler<DeleteContactSettingsCommand, CommonResponse<ContactSettingsDto>>
    {
        private readonly IContactsRepository _contactsRepository;
        public DeleteContactSettingsHandler(IContactsRepository contactsRepository) => _contactsRepository = contactsRepository;
        public async Task<CommonResponse<ContactSettingsDto>> Handle(DeleteContactSettingsCommand request, CancellationToken cancellationToken)
        {
            var result = await _contactsRepository.DeleteSettings(request.SettingsId, request.ContactId);
            if (result != null)
            {
                return new CommonResponse<ContactSettingsDto>
                {
                    Data = result,
                    Result = new Result { ErrorMessage = string.Empty, ResultNumber = 0 }
                };
            }
            return new CommonResponse<ContactSettingsDto>
            {
                Data = null,
                Result = new Result { ErrorMessage = "Error Updating Contacts Settings", ResultNumber = 1 }
            };
        }
    }
}
