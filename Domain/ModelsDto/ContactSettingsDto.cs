using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ModelsDto
{
    public class ContactSettingsDto : BaseEntityDto
    {
        public ContactSettingsTypeDto ContactSettingsType { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public bool IsDefault { get; set; }
    }

    public enum ContactSettingsTypeDto
    {
        Other,
        PaymentMethod,
        ExpirationDate,
        DiscountRate,
        FeeAmount
    }
}
