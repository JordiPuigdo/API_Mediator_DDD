using Domain.ModelsDto;
using Domain.Requests.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Accounts
{
    public interface IAccountsRepository
    {
        Task<IEnumerable<AccountDto>> GetAccounts(AccountTypeDto accountType);
        Task<AccountDto> CreateAccount(NewAccountRequest request);
        Task<AccountDto> DeleteAccount(string id);
        Task<AccountDto> UpdateAccount(UpdateAccountRequest request);

    }
}
