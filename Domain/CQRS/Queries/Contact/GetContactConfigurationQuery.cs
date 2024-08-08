using Domain.Abstractions.Messaging;
using Domain.ModelsDto;
using Domain.Requests.Contact;
using Domain.Responses;
using MediatR;
using System.Numerics;
using System.Xml.Linq;

namespace Domain.CQRS.Queries.Contact
{
    public class GetContactConfigurationQuery : IRequest<CommonResponse<IEnumerable<ContactSettingsDto>>>, ICacheable
    {
        public ContactSettingsTypeDto ContactSettingType { get; set; }
        public string ContactId { get; set; }
        public GetContactConfigurationQuery(GetContactConfigurationRequest request)
        {
            ContactSettingType = request.ContactSettingsType;
            ContactId = request.ContactId ?? string.Empty;  
        }

        public string CacheKey => $"GetContactConfigurationQuery_{ContactSettingType}_{ContactId}";
        public bool BypassCache { get; set; } = false;
        public int SlidingExpirationInMinutes { get; set; } = 30;
        public int AbsoluteExpirationInMinutes { get; set; } = 60;
    }

}
