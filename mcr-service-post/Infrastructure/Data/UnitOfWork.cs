using mcr_service_post.Domain.Interfaces;
using mcr_service_post.Infrastructure.Repositories;
using mcr_service_user.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mcr_service_post.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PostDbContext _context;

        public IPostRepository _postRepository;

        public IPostRepository PostRepository
        {
            get
            {
                return _postRepository = _postRepository ?? new PostRepository(_context);
            }
        }

        public UnitOfWork(PostDbContext context)
        {
            _context = context;
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
