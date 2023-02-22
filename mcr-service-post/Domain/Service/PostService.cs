using mcr_service_post.Domain.Interfaces;
using mcr_service_post.Domain.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mcr_service_post.Domain.Service
{
    public class PostService : IPostRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<PostService> _logger;

        public PostService(IUnitOfWork unitOfWork, ILogger<PostService> logger)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<Post>> ListPostAsync()
        {
            return await _unitOfWork.PostRepository.ListPostAsync();
        }

        public async Task<Post> AddPostAsync(Post post)
        {
            try
            {
                var result = await _unitOfWork.PostRepository.AddPostAsync(post);
                await _unitOfWork.CommitAsync();
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error when create post {ex}", ex.Message);
                throw;
            }
        }
    }
}
