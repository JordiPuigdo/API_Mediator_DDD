using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ModelsDto
{
    public class AccountDto : BaseEntityDto
    {
        public AccountTypeDto AccountType { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }

    public enum AccountTypeDto
    {
        Others,
        Sales,
        Shopping
    }

}
