using Domain.ModelsDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Requests.Contact
{
    public class GetContactWithFiltersRequest
    {
        public string ContactId { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Name { get; set; }
        public ContactTypeDto Type { get; set; }

    }
}
