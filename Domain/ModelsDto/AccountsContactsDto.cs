using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ModelsDto
{
    public class AccountsContactsDto : BaseEntityDto
    {
        public virtual AccountDto Account { get; set; }
        public virtual AccountDto Contact { get; set; }
    }
}
