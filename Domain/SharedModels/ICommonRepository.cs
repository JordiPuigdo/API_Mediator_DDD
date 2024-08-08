using Domain.Abstractions.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.SharedModels
{
    public interface ICommonRepository : IProvider
    {
        Task<T> GetAsync<T>(string id);
        Task<IEnumerable<T>> GetAll<T>();
        Task<T> Create<T>(T entity);
        Task<T> Update<T>(T entity);
        Task<bool> Delete<T>(string id);
    }
}
