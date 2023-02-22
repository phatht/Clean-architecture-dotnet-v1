using mcr_service_user.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mcr_service_user.Domain.Interfaces
{
    public interface IUserRepository
    {
        public Task<IEnumerable<User>> ListUserAsync();

        //public Task<User> FindAsync(Guid id); 
        public Task<User> AddAsync(User user);

        //public Task<User> Updatesync(Guid Id,User user);

        //public Task<User> Removesync(User user);
    }
}
