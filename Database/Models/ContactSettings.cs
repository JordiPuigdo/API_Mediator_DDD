using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Models
{
    public class ContactSettings : BaseEntity
    {
        public ContactSettingsType ContactSettingsType { get; set; }  
        public string Code { get; set; }
        public string Description { get; set; }
        public bool IsDefault { get; set; }
    }

    public enum ContactSettingsType
    {
        Other,
        PaymentMethod,
        ExpirationDate,
        PayDay
    }
}
