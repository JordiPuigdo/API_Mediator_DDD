using AutoMapper;
using Database.Models;
using Domain.Interfaces.Accounts;
using Domain.ModelsDto;
using Domain.Requests.Account;
using Microsoft.EntityFrameworkCore;


namespace Database.Repositories
{
    public class AccountsRepository : IAccountsRepository
    {
        private readonly HubmaSoftContext _context;
        private readonly IMapper _mapper;

        public AccountsRepository(HubmaSoftContext context, IMapper mapper) 
        { 
            _context = context;
            _mapper = mapper;
        }

        public async Task<AccountDto> CreateAccount(NewAccountRequest request)
        {
            try
            {
                var account = await _context.Account.Where(x => x.Code == request.Code || x.Name == request.Name).FirstOrDefaultAsync();
                if (account != null && !string.IsNullOrEmpty(account.Code))
                {
                    return _mapper.Map<AccountDto>(account);    
                }
                account = new Account
                {
                    Code = request.Code,
                    Name = request.Name,
                    AccountType = (AccountType)request.Type,
                };
                _context.Account.Add(account);
                await _context.SaveChangesAsync();
                return _mapper.Map<AccountDto>(account);
            }
            catch (Exception ex)
            {
                throw new Exception("Error createAccount", ex);
            }
        }

        public async Task<AccountDto> DeleteAccount(string id)
        {
            try
            {
                var account = await _context.Account.FirstOrDefaultAsync(x => x.Id == Guid.Parse(id));
                if (account != null)
                {
                    _context.Account.Remove(account);
                    await _context.SaveChangesAsync();
                }
                return _mapper.Map<AccountDto>(account);
            }
            catch (Exception ex)
            {
                throw new Exception("Error DeleteAccount", ex);
            }
        }

        public async Task<IEnumerable<AccountDto>> GetAccounts(AccountTypeDto accountType)
        {
            try
            {
                AccountType type = (AccountType)accountType;
                var accounts = await _context.Account.Where(x => x.AccountType == type).ToListAsync();
                return _mapper.Map<IEnumerable<AccountDto>>(accounts);
            }
            catch (Exception ex)
            {
                throw new Exception("Error GetAccounts", ex);
            }
            
        }

        public async Task<AccountDto> UpdateAccount(UpdateAccountRequest request)
        {
            var account = await _context.Account.FirstOrDefaultAsync(x => x.Id == Guid.Parse(request.Id));
            if (account != null)
            {
                account.UpdatingDate = DateTime.UtcNow;
                account.Code = request.Code;
                account.Name = request.Name;
                account.AccountType = (AccountType)request.Type;
                await _context.SaveChangesAsync();
                return _mapper.Map<AccountDto>(account);
            }
            throw new Exception("Error Updating, not found");
        }
    }
}
