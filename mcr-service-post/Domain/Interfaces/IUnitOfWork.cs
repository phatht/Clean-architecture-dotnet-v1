using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mcr_service_post.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IPostRepository PostRepository { get; }
        Task CommitAsync();
        Task RollbackAsync();
    }
}
