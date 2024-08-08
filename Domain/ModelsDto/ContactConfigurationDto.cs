using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ModelsDto
{
    public class ContactConfigurationDto : BaseEntityDto
    {
        public virtual ContactSettingsDto Settings { get; set; }
        public virtual AccountDto Contact { get; set; }
    }
}
