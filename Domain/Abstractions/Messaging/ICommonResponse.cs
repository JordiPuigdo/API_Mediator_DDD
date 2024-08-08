using Domain.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstractions.Messaging
{
    public interface ICommonResponse
    {
        public Result Result { get; set; }
        object Data { get; set; }
    }

    public interface IResult
    {
        public int ResultNumber { get; set; }
        public string ErrorMessage { get; set; }
    }
}
