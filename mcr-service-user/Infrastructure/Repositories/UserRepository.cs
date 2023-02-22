using mcr_service_user.Domain.Interfaces;
using mcr_service_user.Domain.Models;
using mcr_service_user.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mcr_service_user.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(UserDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<User>> ListUserAsync()
        {
            return await _context.User.AsNoTracking().ToListAsync();
        }

        public async Task<User> AddAsync(User user)
        {
            await _context.User.AddAsync(user);
            return user;
        }
    }
}
