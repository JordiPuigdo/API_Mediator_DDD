using Domain.Abstractions.Providers;
using Domain.CQRS.Queries.Contact;
using Domain.CQRS.Query.User;
using Domain.ModelsDto;
using Domain.Responses;
using Domain.SharedModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Contact
{
    public class GetAllContactsHandler : IRequestHandler<GetAllContactsQuery, CommonResponse<IEnumerable<AccountDto>>>
    {
        private readonly IEnumerable<ICommonRepository> _repository;

        public GetAllContactsHandler(IEnumerable<ICommonRepository> repository)
        {
            _repository = repository;
        }

        public async Task<CommonResponse<IEnumerable<AccountDto>>> Handle(GetAllContactsQuery request, CancellationToken cancellationToken)
        {
            if(string.IsNullOrEmpty(request.ContactId))
            {
                var contacts = await _repository.First(x => x.Provider == Providers.Contact).GetAll<AccountDto>();
                contacts = contacts.Where(x => x.Active == true).ToList();
                return new CommonResponse<IEnumerable<AccountDto>>
                {
                    Data = contacts,
                    Result = new Result { ErrorMessage = string.Empty, ResultNumber = 0 }
                };
            }
            var contact = await _repository.First(x => x.Provider == Providers.Contact).GetAsync<AccountDto>(request.ContactId);
            return new CommonResponse<IEnumerable<AccountDto>>
            {
                Data = new List<AccountDto> { contact },
                Result = new Result { ErrorMessage = string.Empty, ResultNumber = 0 }
            };

        }
    }
}
