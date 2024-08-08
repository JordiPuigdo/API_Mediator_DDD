using Domain.ModelsDto;
using Domain.Requests.ContactSettings;
using Domain.Responses;
using MediatR;

namespace Domain.CQRS.Command.ContactSettings
{
    public class CreateContactSettingsCommand : IRequest<CommonResponse<ContactSettingsDto>>
    {
        public string ContactId { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public ContactSettingsTypeDto ContactSettingsType { get; set; }
        public CreateContactSettingsCommand(CreateContactSettingsRequest request) 
        { 
            ContactId = request.ContactId;
            ContactSettingsType = request.ContactSettingsType;
            Description = request.Description;
            Code = request.Code;
        }
    }
}
