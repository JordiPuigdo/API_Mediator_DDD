using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ModelsDto
{
    public class BaseEntityDto
    {
        public Guid Id { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        [DefaultValue("true")]
        public bool Active { get; set; } = true;
        public DateTime UpdatingDate { get; set; }
    }
}
