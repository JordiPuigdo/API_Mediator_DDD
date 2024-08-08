using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Authentication.Response
{
    public class LoginResponse
    {
        public string Token { get; set; } = "";
        public string RefreshToken { get; set; } = "";
        public DateTime RefreshTokenExpiryTime { get; set; }
        public string AgentId { get; set; } = "";
    }
}
