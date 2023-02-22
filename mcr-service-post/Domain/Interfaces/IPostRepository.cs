using mcr_service_post.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mcr_service_post.Domain.Interfaces
{
    public interface IPostRepository
    {
        public Task<IEnumerable<Post>> ListPostAsync(); 
        public Task<Post> AddPostAsync(Post post);
    }
}
