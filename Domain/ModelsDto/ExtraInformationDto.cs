using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ModelsDto
{
    public class ExtraInformationDto: BaseEntityDto
    {
        public ContactDto Contact { get; set; }
        public decimal SalesTax { get; set; }
        public decimal EquivalenceSurcharge { get; set; }
        public decimal ShoppingTax { get; set; }
        public int PaymentDay { get; set; }
    }
}
