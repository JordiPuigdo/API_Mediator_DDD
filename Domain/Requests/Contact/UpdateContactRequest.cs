using Domain.ModelsDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Requests.Contact
{
    public class UpdateContactRequest : NewContactRequest
    {
        public string? ContactId {get; set;}
        public decimal? SalesTax { get; set; }
        public decimal? EquivalenceSurcharge { get; set; }
        public decimal? ShoppingTax { get; set; }
        public int? PaymentDay { get; set; }
        public string? Tags { get; set; }
        public List<ContactConfigurationDto>? ContactConfiguration { get; set; }
        public List<AccountsContactsDto>? Accounts { get; set; }
    }
}
