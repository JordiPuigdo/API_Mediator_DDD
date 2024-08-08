using Domain.ModelsDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Responses.User
{
    public class UserRegisterResponse : UserDto
    {
        public string Token {  get; set; }
    }
}
