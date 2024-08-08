using Domain.ModelsDto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Models
{
    public class Contact : BaseEntity
    {
        public ContactType ContactType { get; set; }
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
        public string Tags { get; set; }
        public Guid UserOwnerId { get; set; }
        public virtual User? UserOwner { get; set; }
        [InverseProperty("LinkedContact")]
        public virtual List<LinkedContacts>? LinkedContacts { get; set; }
        public virtual List<ContactConfiguration>? ContactConfiguration { get; set; }
        public virtual List<AccountsContacts>? Accounts { get; set; }
        public virtual ExtraInformation? ExtraInformation { get; set; }
    }

    public enum ContactType
    {
        Others,
        Client,
        Provider,
        Lead,
        Debtor,
        Creditor
    }
}
