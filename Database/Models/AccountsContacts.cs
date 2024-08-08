using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Models
{
    public class AccountsContacts : BaseEntity
    {
        public virtual Account Account { get; set; }
        public virtual Contact Contact { get; set; }
    }
}
