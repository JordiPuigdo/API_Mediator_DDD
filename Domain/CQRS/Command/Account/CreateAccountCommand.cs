using Domain.ModelsDto;
using Domain.Requests.Account;
using Domain.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CQRS.Command.Account
{
    public class CreateAccountCommand : IRequest<CommonResponse<AccountDto>>
    {
        public NewAccountRequest NewAccount {  get; set; }

        public CreateAccountCommand(NewAccountRequest newAccount)
        {
            NewAccount = newAccount;
        }

    }
}
