using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Authentication.Request
{

    public sealed record LoginResource(string username, string password);

}
