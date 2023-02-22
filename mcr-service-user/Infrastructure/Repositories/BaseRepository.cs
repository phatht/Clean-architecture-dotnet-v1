using mcr_service_user.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mcr_service_user.Infrastructure.Repositories
{
    public abstract class BaseRepository
    {
        protected readonly UserDbContext _context;

        public BaseRepository(UserDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
    }
}
