using Domain.ModelsDto;
using Domain.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.Users
{
    public interface IUsersRepository
    {
        Task <IEnumerable<UserDto>> GetByFilters(UserFilters filters);
        Task <UserDto> AddContact(ContactDto contact , string userId);
    }
}
