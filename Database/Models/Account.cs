using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Models
{
    public class Account : BaseEntity
    {
        public AccountType AccountType { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }    
    }

    public enum AccountType
    {
        Others,
        Sales,
        Shopping
    }
}
