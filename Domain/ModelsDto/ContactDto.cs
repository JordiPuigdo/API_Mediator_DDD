using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ModelsDto
{
    public class ContactSuperSimpleDto : BaseEntityDto
    {
        public ContactTypeDto ContactType { get; set; }
        public string Name { get; set; }
        public string NIE { get; set; }
        public string Address { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Phone1 { get; set; }
        public string Website { get; set; }
        public string VatIdentification { get; set; }
        public string CommercialName { get; set; }
        public string TaxNumber { get; set; }
        public string Tags { get; set; }
    }

    public enum ContactTypeDto
    {
        Others,
        Client,
        Provider,
        Lead,
        Debtor,
        Creditor
    }
    public class ContactDto : ContactSuperSimpleDto
    {
        public UserDto? UserOwner { get; set; }
        public List<LinkedContactDto>? LinkedContacts { get; set; }
        public List<ContactConfigurationDto>? ContactConfiguration { get; set; }
        public List<AccountsContactsDto>? Accounts { get; set; }
        public ExtraInformationDto? ExtraInformation { get; set; }
    }

  
}
