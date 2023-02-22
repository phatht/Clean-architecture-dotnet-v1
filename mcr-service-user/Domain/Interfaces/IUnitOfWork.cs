using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mcr_service_user.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository UserRepository { get; }
        Task CommitAsync(); 
        Task RollbackAsync();
    }
}
