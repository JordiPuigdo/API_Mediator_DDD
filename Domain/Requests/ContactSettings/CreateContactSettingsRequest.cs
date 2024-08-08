using Domain.ModelsDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Requests.ContactSettings
{
    public class CreateContactSettingsRequest
    {
        public string ContactId { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public ContactSettingsTypeDto ContactSettingsType { get; set; }
    }
}
