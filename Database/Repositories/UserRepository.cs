using AutoMapper;
using Database.Models;
using Domain.Abstractions.Providers;
using Domain.Interfaces.Users;
using Domain.ModelsDto;
using Domain.SharedModels;
using Domain.User;
using Microsoft.EntityFrameworkCore;

namespace Database.Repositories
{
    public class UserRepository : ICommonRepository, IUsersRepository
    {

        private readonly HubmaSoftContext _context;
        private readonly IMapper _mapper;

        public UserRepository(HubmaSoftContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public string Provider => Providers.User;

        public async Task<T> GetAsync<T>(string id)
        {
            Guid.TryParse(id, out var userId);
            var user = await _context.Users.FindAsync(userId);
            return _mapper.Map<T>(user);
        }

        public async Task<IEnumerable<T>> GetAll<T>()
        {
            var users = await _context.Users.ToListAsync();
            return _mapper.Map<IEnumerable<T>>(users);
        }

        public async Task<T> Create<T>(T entity)
        {
            var user = _mapper.Map<User>(entity);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return _mapper.Map<T>(user);
        }

        public async Task<T> Update<T>(T entity)
        {
            var user = _mapper.Map<User>(entity);
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return _mapper.Map<T>(user);
        }

        public async Task<bool> Delete<T>(string id)
        {
            Guid.TryParse(id, out var userId);
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return false;
            }
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task <IEnumerable<UserDto>> GetByFilters(UserFilters filters)
        {
            var users = new List<User>();
            if(!String.IsNullOrEmpty(filters.Username))
            {
                users = await _context.Users.Where(x => x.UserName == filters.Username).ToListAsync();
                if(users.Any())
                {
                    return _mapper.Map<List<UserDto>>(users);
                }
                
            }

            if (!String.IsNullOrEmpty(filters.Email))
            {
                users = await _context.Users.Where(x =>  Microsoft.EntityFrameworkCore.EF.Functions.Like(x.Email.ToUpper(), $"%{filters.Email.ToUpper()}%")).ToListAsync();
                if(users.Any())
                {
                    return _mapper.Map<List<UserDto>>(users);
                }
            }
            return _mapper.Map<List<UserDto>>(users);
        }

        public async Task<UserDto> AddContact(ContactDto contact, string userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == Guid.Parse(userId));
            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {userId} not found.");
            }
            contact.UserOwner = _mapper.Map<UserDto>(user);
            _context.Contacts.Add(_mapper.Map<Contact>(contact));

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return _mapper.Map<UserDto>(user);
        }

    }
}
