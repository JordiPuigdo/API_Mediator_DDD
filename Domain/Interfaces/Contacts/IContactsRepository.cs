using Domain.ModelsDto;
using Domain.Requests.Contact;
using Domain.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Contacts
{
    public interface IContactsRepository
    {
        Task<IEnumerable<ContactDto>> GetByFilters(GetContactWithFiltersRequest filters);

        Task<IEnumerable<ContactSettingsDto>> GetContactsConfigurations(GetContactConfigurationRequest request); 

        Task<ContactDto> AddExtraInformation(UpdateContactRequest request);

        Task<ContactSettingsDto> AddSettings(ContactSettingsDto request, string contactId);
        Task<ContactSettingsDto> DeleteSettings(string settingsId, string contactId);

    }
}
