using Domain.CQRS.Queries.Account;
using Domain.CQRS.Queries.Contact;
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
    public class GetAccountsByTypeHandler : IRequestHandler<GetAccountsByTypeQuery, CommonResponse<IEnumerable<AccountDto>>>
    {
        private readonly IAccountsRepository _accountRepository;

        public GetAccountsByTypeHandler(IAccountsRepository accountsRepository)
        {
            _accountRepository = accountsRepository;
        }

        public async Task<CommonResponse<IEnumerable<AccountDto>>> Handle(GetAccountsByTypeQuery request, CancellationToken cancellationToken)
        {
            var result = await _accountRepository.GetAccounts(request.AccountType);
            if (result.Any())
            {
                return new CommonResponse<IEnumerable<AccountDto>> { Data = result, Result = new Result { ErrorMessage = string.Empty, ResultNumber = 0 } };
            }
            return new CommonResponse<IEnumerable<AccountDto>> { Data = result, Result = new Result { ErrorMessage = "No data", ResultNumber = 0 } };
        }
    }
}
