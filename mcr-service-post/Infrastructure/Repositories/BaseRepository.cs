using mcr_service_user.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mcr_service_post.Infrastructure.Repositories
{
    public abstract class BaseRepository
    {
        protected readonly PostDbContext _context;

        public BaseRepository(PostDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
    }
}
