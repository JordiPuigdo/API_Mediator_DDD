using Domain.Authentication;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Application.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAuthoritzationTokenUrls _authURLs;

        public AuthenticationService(IAuthoritzationTokenUrls authURLs)
        {
            _authURLs = authURLs;
        }
        public async Task<bool> AuthenticateTokenAsync(string token, string url)
        {

            if (!RequireAuthentication(url))
            {
                return true;
            }
            if (token == null)
            {       
                return false;
            }
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes("asdfjasldkfhosafoihoqwjernlkqwer");
                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = "HubmaSoftAPI",
                    ValidAudience = "CRM",
                    ValidateLifetime = true
                };

                SecurityToken validatedToken;
                var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out validatedToken);
                return true;
            }
            catch (SecurityTokenException)
            {
                return false;
            }
        }

        private bool RequireAuthentication(string url)
        {
            if(_authURLs.Urls.Contains(url))
            {
                return true;
            }

           

            return false;
        }
    }
}
