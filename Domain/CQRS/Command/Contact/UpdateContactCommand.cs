using Domain.Abstractions.Messaging;
using Domain.ModelsDto;
using Domain.Requests.Contact;
using Domain.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Domain.CQRS.Command.Contact
{
    public class UpdateContactCommand : IRequest<CommonResponse<ContactDto>>
    { 
        public UpdateContactRequest UpdateContactRequest { get; set; }

        public UpdateContactCommand(UpdateContactRequest request)
        {
           UpdateContactRequest = request;
        }
    }
}
