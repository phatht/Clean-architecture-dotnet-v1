using mcr_service_user.Domain.Interfaces;
using mcr_service_user.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mcr_service_user.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly UserDbContext _context;

        public IUserRepository _userRepository;
         
        public UnitOfWork(UserDbContext context)
        {
            _context = context;
        }

        public IUserRepository UserRepository
        {
            get
            {
                return _userRepository = _userRepository ?? new UserRepository(_context);
            }
        }
        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task RollbackAsync()
        {
            await _context.DisposeAsync();
        }

    }
}
