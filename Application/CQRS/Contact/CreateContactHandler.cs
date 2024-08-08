using Application.CQRS.User;
using Domain.Abstractions;
using Domain.Abstractions.Providers;
using Domain.CQRS.Command.Contact;
using Domain.CQRS.Queries.Contact;
using Domain.CQRS.Query.User;
using Domain.Interfaces.Contacts;
using Domain.ModelsDto;
using Domain.Requests.Contact;
using Domain.Responses;
using Domain.SharedModels;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Contact
{
    public class CreateContactHandler : IRequestHandler<CreateContactCommand, CommonResponse<ContactDto>>
    {
        private readonly IEnumerable<ICommonRepository> _repositoryFactory;
        private readonly IMediator _mediator;
        public CreateContactHandler(IEnumerable<ICommonRepository> repositoryFactory,  IMediator mediator)
        {
            _repositoryFactory = repositoryFactory;
            _mediator = mediator;

        }

        public async Task<CommonResponse<ContactDto>> Handle(CreateContactCommand request, CancellationToken cancellationToken)
        {
            var response = new CommonResponse<ContactDto>();
            try
            {
                var contactRepository = _repositoryFactory.First(x => x.Provider == Providers.Contact);

                var filterRequest = new GetContactWithFiltersRequest
                {
                    Name = request.Name,
                    Email = request.Email,
                    Phone = request.Phone
                };

                var query = new GetContactWithFiltersQuery(filterRequest);

                var contactsResponse = await _mediator.Send(query, cancellationToken);

                if (contactsResponse.Result.ResultNumber == 0 && !contactsResponse.Data.Any())
                {
                    var userQuery = new GetUserByIdQuery(request.UserId);
                    var user = await _mediator.Send(userQuery, cancellationToken);
                    var result = await _repositoryFactory.First(x => x.Provider == Providers.Contact).Create(new ContactDto
                    {
                        Phone = string.IsNullOrEmpty(request.Phone) ? string.Empty : request.Phone,
                        Name = string.IsNullOrEmpty(request.Name) ? string.Empty : request.Name,
                        Email = string.IsNullOrEmpty(request.Email) ? string.Empty : request.Email,
                        Address = string.IsNullOrEmpty(request.Address) ? string.Empty : request.Address,
                        City = string.IsNullOrEmpty(request.City) ? string.Empty : request.City,
                        Country = string.IsNullOrEmpty(request.Country) ? string.Empty : request.Country,
                        ContactType = request.ContactType,
                        Phone1 = string.IsNullOrEmpty(request.Phone1) ? string.Empty : request.Phone1,
                        PostalCode = string.IsNullOrEmpty(request.PostalCode) ? string.Empty : request.PostalCode,
                        VatIdentification = string.IsNullOrEmpty(request.TaxNumber) ? string.Empty : request.TaxNumber,
                        Website = string.IsNullOrEmpty(request.Website) ? string.Empty : request.Website,
                        CommercialName = string.IsNullOrEmpty(request.CommercialName) ? string.Empty : request.CommercialName,
                        NIE = string.IsNullOrEmpty(request.NIE) ? string.Empty : request.NIE,
                        Tags = "",
                        Province = string.IsNullOrEmpty(request.Province) ? string.Empty : request.Province,
                        UserOwner = user.Data
                    });

                    await _mediator.Send(new RemoveCacheEntryCommand("ContactsAll"), cancellationToken);
                    
                    return new CommonResponse<ContactDto>
                    {
                        Data = result,
                        Result = new Result { ErrorMessage = string.Empty, ResultNumber = 0 }
                    };

                }
                return new CommonResponse<ContactDto>
                {
                    Data = null,
                    Result = new Result { ErrorMessage = "Existing Contact", ResultNumber = 1 }
                };
            }
            catch (Exception ex)
            {

            }


           return response;
        }
    }

}
