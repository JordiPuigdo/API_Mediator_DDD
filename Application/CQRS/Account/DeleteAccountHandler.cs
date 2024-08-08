using Domain.CQRS.Command.Account;
using Domain.Interfaces.Accounts;
using Domain.ModelsDto;
using Domain.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Account
{
    public class DeleteAccountHandler : IRequestHandler<DeleteAccountCommand, CommonResponse<AccountDto>>
    {
        private readonly IAccountsRepository _accountsRepository;
        public DeleteAccountHandler(IAccountsRepository accounts) => _accountsRepository = accounts;
        public async Task<CommonResponse<AccountDto>> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
        {
            var result = await _accountsRepository.DeleteAccount(request.Id);
            if (result != null)
            {
                return new CommonResponse<AccountDto> { Data = result, Result = new Result { ErrorMessage = "", ResultNumber = 0 } };
            }
            return new CommonResponse<AccountDto> { Data = null, Result = new Result { ErrorMessage = "Error deleting account", ResultNumber = 1 } };

        }
    }
}
