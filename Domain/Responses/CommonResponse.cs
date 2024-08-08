using Domain.Abstractions.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Responses
{
    public class CommonResponse<T> : ICommonResponse
    {
        public Result Result { get; set; }
        public T Data { get; set; }

        object ICommonResponse.Data
        {
            get => Data;
            set => Data = (T)value;
        }
    }

    public class Result : IResult
    {
        public int ResultNumber { get; set; }
        public string ErrorMessage { get; set; }
    }
}
