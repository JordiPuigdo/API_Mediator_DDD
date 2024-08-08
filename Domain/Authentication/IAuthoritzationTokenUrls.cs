using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Authentication
{
    public interface IAuthoritzationTokenUrls
    {
        public List<string> Urls { get; set; }
    }
}
