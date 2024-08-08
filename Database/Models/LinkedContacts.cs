using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Models
{
    public class LinkedContacts : BaseEntity
    {
        public Guid? OwnerContactId { get; set; }
        [ForeignKey("OwnerContactId")]
        public virtual Contact Contact{ get; set; }
        public Guid? LinkedContactId { get; set; }
        [ForeignKey("LinkedContactId")]
        public virtual Contact LinkedContact{ get; set; }
        [DefaultValue("false")]
        public bool IsApproved { get; set; }
    }
}
