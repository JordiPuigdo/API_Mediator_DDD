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

namespace Domain.CQRS.Command.Contact
{
    public class CreateContactCommand : IRequest<CommonResponse<ContactDto>>, ICacheable
    {

        public bool BypassCache { get; set; } = false;
        public int SlidingExpirationInMinutes { get; set; } = 30;
        public int AbsoluteExpirationInMinutes { get; set; } = 60;
        public ContactTypeDto ContactType { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Phone1 { get; set; }
        public string Website { get; set; }
        public string TaxNumber { get; set; }
        public string UserId { get; set; }
        public string CommercialName { get; set; }
        public string NIE { get; set; }
        public string Province { get; set; }
        public string CacheKey => $"CreateContactCommand_{Name}_{Email}_{Phone}";

        public CreateContactCommand(NewContactRequest newContact) 
        {
            ContactType = (ContactTypeDto)newContact.ContactType;
            Name = newContact.Name;
            Address = newContact.Address;
            Country = newContact.Country ?? string.Empty;
            City = newContact.City == null ? string.Empty : newContact.City;
            PostalCode = newContact.PostalCode == null ? string.Empty : newContact.PostalCode;
            Email = newContact.Email == null ? string.Empty : newContact.Email;
            Phone = newContact.Phone == null ? string.Empty : newContact.Phone;
            Phone1 = newContact.Phone1 == null ? string.Empty : newContact.Phone1;
            Website = newContact.Website == null ? string.Empty : newContact.Website;
            TaxNumber = newContact.TaxNumber == null ? string.Empty : newContact.TaxNumber;
            UserId = newContact.UserId;
            Province = newContact.Province ?? string.Empty;
            NIE = newContact.NIE ?? string.Empty;
            CommercialName = newContact.CommercialName ?? string.Empty; 
        }
    }
}
