using Domain.Authentication;

namespace HubmaSoftAPI.Models
{
    public class UrlsAuthentication : IAuthoritzationTokenUrls
    {
        public List<string> Urls { get; set; }

    }
}
