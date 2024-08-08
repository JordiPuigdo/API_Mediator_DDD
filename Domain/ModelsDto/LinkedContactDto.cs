using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ModelsDto
{
    public class LinkedContactDto : BaseEntityDto
    {
        public Guid? OwnerContactId { get; set; }
        
        public AccountDto Contact { get; set; }
        public Guid? LinkedContactId { get; set; }
        public virtual AccountDto LinkedContact { get; set; }
    }
}
