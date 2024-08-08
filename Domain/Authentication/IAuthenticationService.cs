using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Authentication
{
    public interface IAuthenticationService
    {
        Task<bool> AuthenticateTokenAsync(string token, string url);
    }
}
