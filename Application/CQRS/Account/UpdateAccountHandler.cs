using Domain.CQRS.Command.Account;
using Domain.Interfaces.Accounts;
using Domain.ModelsDto;
using Domain.Requests.Contact;
using Domain.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Account
{
    public class UpdateAccountHandler : IRequestHandler<UpdateAccountCommand, CommonResponse<AccountDto>>
    {
        private readonly IAccountsRepository _accountsRepository;

        public UpdateAccountHandler(IAccountsRepository accountsRepository) => _accountsRepository = accountsRepository;
        public async Task<CommonResponse<AccountDto>> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
        {
            var result = await _accountsRepository.UpdateAccount(request.UpdateAccountRequest);
            if (result != null)
            {
                return new CommonResponse<AccountDto> { Data = result, Result = new Result { ErrorMessage = string.Empty, ResultNumber = 0 } };
            }
            return new CommonResponse<AccountDto> { Data = null, Result = new Result { ErrorMessage = "Error Update Account", ResultNumber = 1 } };

        }
    }
}
