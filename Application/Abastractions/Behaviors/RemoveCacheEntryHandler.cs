using Application.Abstractions.Behaviors;
using Domain.Abstractions;
using Domain.CQRS.Queries.Contact;
using Domain.ModelsDto;
using Domain.Responses;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abastractions.Behaviors
{
    public class RemoveCacheEntryHandler : IRequestHandler<RemoveCacheEntryCommand, Unit>
    {
        private readonly IDistributedCache _cache;

        public RemoveCacheEntryHandler(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task<Unit> Handle(RemoveCacheEntryCommand request, CancellationToken cancellationToken)
        {
            await _cache.RemoveAsync(request.CacheKey, cancellationToken);
            return Unit.Value;
        }
    }

}
