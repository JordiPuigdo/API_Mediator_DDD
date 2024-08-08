using AutoMapper;
using Database.Models;
using Domain.Abstractions.Providers;
using Domain.Interfaces.Contacts;
using Domain.ModelsDto;
using Domain.Requests.Contact;
using Domain.SharedModels;
using Microsoft.EntityFrameworkCore;

namespace Database.Repositories
{
    public class ContactRepository : ICommonRepository, IContactsRepository
    {
        private readonly HubmaSoftContext _context;
        private readonly IMapper _mapper;

        
        public ContactRepository(HubmaSoftContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public string Provider => Providers.Contact;
        public async Task<T> Create<T>(T entity)
        {
            var contact = _mapper.Map<Contact>(entity);
            if (contact.UserOwner != null)
            {
                var userId = contact.UserOwner.Id;
                contact.UserOwner = null;
                contact.UserOwnerId = userId;
            }
            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();
            return _mapper.Map<T>(contact);
        }

        public async Task<bool> Delete<T>(string id)
        {
            Guid.TryParse(id, out var contactId);
            var contact = await _context.Contacts.FindAsync(contactId);
            if (contact == null)
            {
                return false;
            }
            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<T>> GetAll<T>()
        {
            var contacts = await _context.Contacts.ToListAsync();
            return _mapper.Map<IEnumerable<T>>(contacts);
        }

        public async Task<T> GetAsync<T>(string id)
        {
            Guid.TryParse(id, out var contactId);
            var contacts = await _context.Contacts.FindAsync(contactId);
            return _mapper.Map<T>(contacts);
        }

        public async Task<T> Update<T>(T entity)
        {
            var contact = _mapper.Map<Contact>(entity);
            _context.Contacts.Update(contact);
            await _context.SaveChangesAsync();
            return _mapper.Map<T>(contact);
        }

        public async Task<IEnumerable<ContactDto>> GetByFilters(GetContactWithFiltersRequest filters)
        {
            var contacts = new List<Contact>();
            if (!String.IsNullOrEmpty(filters.ContactId))
            {
                contacts = await _context.Contacts.Where(x => x.Id == Guid.Parse(filters.ContactId)).ToListAsync();
                if (contacts.Any())
                {
                    return _mapper.Map<List<ContactDto>>(contacts);
                }

            }
            if (!String.IsNullOrEmpty(filters.Email))
            {
                contacts = await _context.Contacts.Where(x => x.Email == filters.Email).ToListAsync();
                if (contacts.Any())
                {
                    return _mapper.Map<List<ContactDto>>(contacts);
                }

            }

            if (!String.IsNullOrEmpty(filters.Email))
            {
                contacts = await _context.Contacts.Where(x => Microsoft.EntityFrameworkCore.EF.Functions.Like(x.Name.ToUpper(), $"%{filters.Name.ToUpper()}%")).ToListAsync();
                if (contacts.Any())
                {
                    return _mapper.Map<List<ContactDto>>(contacts);
                }
            }

            if (!String.IsNullOrEmpty(filters.Phone))
            {
                contacts = await _context.Contacts.Where(x => x.Phone == filters.Phone).ToListAsync();
                if (contacts.Any())
                {
                    return _mapper.Map<List<ContactDto>>(contacts);
                }

            }

            return _mapper.Map<List<ContactDto>>(contacts);
        }

        public async Task<IEnumerable<ContactSettingsDto>> GetContactsConfigurations(GetContactConfigurationRequest request)
        {
            
            if (string.IsNullOrEmpty(request.ContactId))
            {
                var config = await _context.ContactSettings.Where(x => x.IsDefault == true && x.ContactSettingsType == (ContactSettingsType)request.ContactSettingsType).ToListAsync();
                return _mapper.Map<IEnumerable<ContactSettingsDto>>(config);
            }
            else
            {
                var contactId = Guid.Parse(request.ContactId);
                var settingsType = (ContactSettingsType)request.ContactSettingsType;

                var contactSettings = await _context.ContactConfigurations
                    .Include(x => x.Settings) 
                    .Where(x => x.Settings.ContactSettingsType == settingsType &&
                                (x.Contact.Id == contactId || x.Settings.IsDefault))
                    .Select(x => x.Settings)
                    .ToListAsync();

                var defaultSettings = await _context.ContactSettings
                    .Where(x => x.IsDefault && x.ContactSettingsType == settingsType)
                    .ToListAsync();

                var combinedSettings = defaultSettings.Concat(contactSettings)
                    .DistinctBy(s => s.Id) 
                    .ToList();

                return _mapper.Map<IEnumerable<ContactSettingsDto>>(combinedSettings);
            }
        }

        public async Task<ContactDto> AddExtraInformation(UpdateContactRequest request)
        {
            var contact = new Contact();
            contact = await _context.Contacts
                .Include(c => c.Accounts)
                .Include(c => c.ContactConfiguration)
                .Include(c => c.ExtraInformation)
              .FirstOrDefaultAsync(x => x.Id == Guid.Parse(request.ContactId));

            if (contact == null)
            {
                throw new KeyNotFoundException("Contact not found");
            }

            var contactExtraInformation = new ExtraInformation()
            {
                EquivalenceSurcharge = (decimal)(request.EquivalenceSurcharge != null ? request.EquivalenceSurcharge : 0),
                PaymentDay = (int)(request.PaymentDay!= null ? request.PaymentDay : 0),
                SalesTax = (decimal)(request.SalesTax != null ? request.SalesTax : 0),
                ShoppingTax = (decimal)(request.ShoppingTax != null ? request.ShoppingTax : 0),
            };
            if(contact.ExtraInformation != null)
            {
                _context.ExtraInformation.Remove(contact.ExtraInformation);
            }
      
            contact.ExtraInformation = contactExtraInformation;
            
                

            if (request.Accounts != null)
            {
                contact.Accounts?.Clear();
                var mappedAccounts = _mapper.Map<AccountsContacts>(request.Accounts);
                contact.Accounts = mappedAccounts != null ? new List<AccountsContacts> { mappedAccounts } : null;
            }

            if (request.ContactConfiguration != null)
            {
                contact.ContactConfiguration?.Clear();
                var mappedContactConfig = _mapper.Map<ContactConfiguration>(request.ContactConfiguration);
                contact.ContactConfiguration = mappedContactConfig != null ? new List<ContactConfiguration> { mappedContactConfig } : null;
            }

            await _context.SaveChangesAsync();


            return _mapper.Map<ContactDto>(contact);
        }

        public async Task<ContactSettingsDto> AddSettings(ContactSettingsDto request, string contactId)
        {
            var contact = await _context.Contacts.FirstOrDefaultAsync(x => x.Id == Guid.Parse(contactId));
            if (contact == null)
            {
                return new ContactSettingsDto();
            }
            var response = _mapper.Map<ContactSettings>(request);

            await _context.ContactConfigurations.AddAsync(new ContactConfiguration
            {
                Contact = contact,
                Settings = response,
            });
            await _context.SaveChangesAsync();
            return request;
        }

        public async Task<ContactSettingsDto> DeleteSettings(string settingsId, string contactId)
        {
            var result = await _context.ContactConfigurations.FirstOrDefaultAsync(x => x.Contact.Id == Guid.Parse(contactId) && x.Settings.Id == Guid.Parse(settingsId));
            if(result != null)
            {
                _context.ContactConfigurations.Remove(result);
               await _context.SaveChangesAsync();
            }
            return _mapper.Map<ContactSettingsDto>(result.Settings);
        }
    }
}
