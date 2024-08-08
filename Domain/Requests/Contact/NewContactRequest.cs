using Domain.ModelsDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Requests.Contact
{
    public class NewContactRequest
    {
        public string UserId { get; set; }
        public ContactTypeDto ContactType { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string? Country { get; set; } = string.Empty;
        public string? City { get; set; } = string.Empty;
        public string? PostalCode { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public string? Phone { get; set; } = string.Empty;
        public string? Phone1 { get; set; } = string.Empty;
        public string? Website { get; set; } = string.Empty;
        public string? TaxNumber { get; set; } = string.Empty;
        public string? CommercialName { get; set; } = string.Empty;
        public string? NIE { get; set; } = string.Empty;
        public string? Province { get; set; } = string.Empty;
    }
}
