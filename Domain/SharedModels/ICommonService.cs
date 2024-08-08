using Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.SharedModels
{
    public interface ICommonService
    {
        Task<CommonResponse<T>> GetAsync<T>(string id);
        Task<CommonResponse<IEnumerable<T>>> GetAll<T>();
        Task<CommonResponse<T>> Create<T>(T entity);
        Task<CommonResponse<T>> Update<T>(T entity);
        Task<CommonResponse<bool>> Delete<T>(string id);
    }
}
