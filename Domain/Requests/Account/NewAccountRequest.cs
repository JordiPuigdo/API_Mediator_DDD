using Domain.ModelsDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Requests.Account
{
    public class NewAccountRequest
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public AccountTypeDto Type { get; set; }
    }

    public class UpdateAccountRequest : NewAccountRequest
    {
        public string Id
        {
            get; set;
        }
    }
}
