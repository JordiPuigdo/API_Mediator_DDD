using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstractions
{
    public class RemoveCacheEntryCommand : IRequest<Unit>
    {
        public string CacheKey { get; }

        public RemoveCacheEntryCommand(string cacheKey)
        {
            CacheKey = cacheKey;
        }
    }

}
