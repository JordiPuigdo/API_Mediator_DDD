using Domain.Abstractions.Providers;
using Domain.CQRS.Command.Contact;
using Domain.CQRS.Queries.Contact;
using Domain.Interfaces.Contacts;
using Domain.ModelsDto;
using Domain.Requests.Contact;
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
    public class UpdateContactHandler : IRequestHandler<UpdateContactCommand, CommonResponse<ContactDto>>
    {
        private readonly IContactsRepository _contactRepository;
        private readonly IMediator _mediator;
        public UpdateContactHandler(IContactsRepository contactsRepository, IMediator mediator)
        {
            _contactRepository = contactsRepository;
            _mediator = mediator;

        }
        public async Task<CommonResponse<ContactDto>> Handle(UpdateContactCommand request, CancellationToken cancellationToken)
        {
            var response = new CommonResponse<ContactDto>();
            try
            {
                
                var filterRequest = new GetContactWithFiltersRequest
                {
                    ContactId = request.UpdateContactRequest.ContactId,
                    Name = request.UpdateContactRequest.Name,
                    Email = request.UpdateContactRequest.Email,
                    Phone = request.UpdateContactRequest.Phone
                };
                var query = new GetContactWithFiltersQuery(filterRequest);
                var contactsResponse = await _mediator.Send(query, cancellationToken);

                if (contactsResponse.Result.ResultNumber == 0 && !contactsResponse.Data.Any())
                {

                }
                var existingContact = contactsResponse.Data.FirstOrDefault();

                var updatedContact = new UpdateContactRequest()
                {
                    ContactId = request.UpdateContactRequest.ContactId,
                    UserId = request.UpdateContactRequest.UserId,
                    Name = GetUpdatedField(request.UpdateContactRequest.Name, existingContact.Name),
                    Email = GetUpdatedField(request.UpdateContactRequest.Email, existingContact.Email),
                    Address = GetUpdatedField(request.UpdateContactRequest.Address, existingContact.Address),
                    City = GetUpdatedField(request.UpdateContactRequest.City, existingContact.City),
                    CommercialName = GetUpdatedField(request.UpdateContactRequest.CommercialName, existingContact.CommercialName),
                    Province = GetUpdatedField(request.UpdateContactRequest.Province, existingContact.Province),
                    PostalCode = GetUpdatedField(request.UpdateContactRequest.PostalCode, existingContact.PostalCode),
                    Country = GetUpdatedField(request.UpdateContactRequest.Country, existingContact.Country),
                    NIE = GetUpdatedField(request.UpdateContactRequest.NIE, existingContact.NIE),
                    Phone = GetUpdatedField(request.UpdateContactRequest.Phone, existingContact.Phone),
                    Phone1 = GetUpdatedField(request.UpdateContactRequest.Phone1, existingContact.Phone1),
                    Website = GetUpdatedField(request.UpdateContactRequest.Website, existingContact.Website),
                    TaxNumber = GetUpdatedField(request.UpdateContactRequest.TaxNumber, existingContact.TaxNumber),
                    Tags = GetUpdatedField(request.UpdateContactRequest.Tags, existingContact.Tags)
                };

                if (existingContact.Accounts != null && existingContact.Accounts.Any())
                {
                    updatedContact.Accounts = existingContact.Accounts; 
                }

                if (existingContact.ContactConfiguration != null && existingContact.ContactConfiguration.Any())
                {
                    updatedContact.ContactConfiguration = existingContact.ContactConfiguration;
                }

                if (existingContact.ExtraInformation != null)
                {
                    updatedContact.EquivalenceSurcharge = request.UpdateContactRequest.EquivalenceSurcharge ?? existingContact.ExtraInformation.EquivalenceSurcharge;
                    updatedContact.PaymentDay = request.UpdateContactRequest.PaymentDay ?? existingContact.ExtraInformation.PaymentDay;
                    updatedContact.SalesTax = request.UpdateContactRequest.SalesTax ?? existingContact.ExtraInformation.SalesTax;
                    updatedContact.ShoppingTax = request.UpdateContactRequest.ShoppingTax ?? existingContact.ExtraInformation.ShoppingTax;
                }
                else
                {
                    existingContact.ExtraInformation = new ExtraInformationDto
                    {
                        Contact = existingContact,
                        EquivalenceSurcharge = request.UpdateContactRequest.EquivalenceSurcharge ?? 0,
                        PaymentDay = request.UpdateContactRequest.PaymentDay ?? 0,
                        SalesTax = request.UpdateContactRequest.SalesTax ?? 0,
                        ShoppingTax = request.UpdateContactRequest.ShoppingTax ?? 0,
                    };
                }
                var contact = await _contactRepository.AddExtraInformation(updatedContact);
                if (contact != null)
                {
                    response.Data = contact;
                    response.Result = new Result { ErrorMessage = string.Empty, ResultNumber = 0 };
                }
            }
            catch (Exception ex)
            {

            }

        

            return response;
        }

        string GetUpdatedField(string newField, string existingField)
        {
            return !string.IsNullOrEmpty(newField) ? newField : existingField;
        }

    }
}
