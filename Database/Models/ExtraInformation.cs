using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Models
{
    public class ExtraInformation : BaseEntity
    {
        public decimal SalesTax {  get; set; }
        public decimal EquivalenceSurcharge { get; set; }
        public decimal ShoppingTax { get; set; }
        public int PaymentDay { get; set; }

    }
}
