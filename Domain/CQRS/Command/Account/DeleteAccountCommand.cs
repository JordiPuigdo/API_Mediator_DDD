using Domain.ModelsDto;
using Domain.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CQRS.Command.Account
{
    public class DeleteAccountCommand : IRequest<CommonResponse<AccountDto>>
    {
        public string Id { get; set; }  

        public DeleteAccountCommand(string id)
        {
            Id = id;
        }
    }
}
