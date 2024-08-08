using Domain.ModelsDto;
using Domain.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CQRS.Command.Contact
{
    public class LinkContactCommand : IRequest<CommonResponse<ContactDto>>
    {
        public string ContactOwnerId { get; set; }
        public string ContactLinkedId { get; set; }
        public LinkContactCommand(string contactOwnerId, string contactLinkedId)
        {
            ContactOwnerId = contactOwnerId;
            ContactLinkedId = contactLinkedId;
        }
    }
}
