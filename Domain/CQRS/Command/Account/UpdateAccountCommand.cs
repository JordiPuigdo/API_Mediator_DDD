using Domain.ModelsDto;
using Domain.Requests.Account;
using Domain.Requests.Contact;
using Domain.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CQRS.Command.Account
{
    public class UpdateAccountCommand : IRequest<CommonResponse<AccountDto>>
    {
        public UpdateAccountRequest UpdateAccountRequest { get; set; }
        public UpdateAccountCommand(UpdateAccountRequest updateAccount)
        {

            UpdateAccountRequest = updateAccount;
        }
    }
}
