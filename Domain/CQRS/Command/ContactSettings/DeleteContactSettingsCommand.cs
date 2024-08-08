using Domain.ModelsDto;
using Domain.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CQRS.Command.ContactSettings
{
    public class DeleteContactSettingsCommand : IRequest<CommonResponse<ContactSettingsDto>>
    {
        public string ContactId { get; set; }   
        public string SettingsId { get; set; }

        public DeleteContactSettingsCommand(string contactId, string settingsId)
        { 
            ContactId = contactId;
            SettingsId = settingsId;
        }
    }
}
