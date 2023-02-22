using mcr_service_post.Domain.Interfaces;
using mcr_service_post.Domain.Models;
using mcr_service_user.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mcr_service_post.Infrastructure.Repositories
{
    public class PostRepository : BaseRepository, IPostRepository
    {
        public PostRepository(PostDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Post>> ListPostAsync()
        {
            return await _context.Post.AsNoTracking().ToListAsync();
        }

        public async Task<Post> AddPostAsync(Post post)
        {
            await _context.Post.AddAsync(post);
            return post;
        }
    }
}
