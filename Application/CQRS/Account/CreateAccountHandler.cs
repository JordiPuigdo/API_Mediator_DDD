using Domain.CQRS.Command.Account;
using Domain.Interfaces.Accounts;
using Domain.ModelsDto;
using Domain.Responses;
using MediatR;


namespace Application.CQRS.Account
{
    public class CreateAccountHandler : IRequestHandler<CreateAccountCommand, CommonResponse<AccountDto>>
    {
        private readonly IAccountsRepository _accountRepository;
        public CreateAccountHandler(IAccountsRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<CommonResponse<AccountDto>> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
        {
            var result = await _accountRepository.CreateAccount(request.NewAccount);
            return new CommonResponse<AccountDto> { Data = result, Result = new Result { ErrorMessage = string.Empty, ResultNumber = 0 } };
        }
    }
}
