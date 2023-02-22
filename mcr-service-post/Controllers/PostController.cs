using Kernels.Infrastructure.Alfresco;
using Kernels.Infrastructure.Cache.Redis;
using mcr_service_post.Domain.Interfaces;
using mcr_service_post.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace mcr_service_post.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _postRepository;
        private readonly ILogger<PostController> _logger;
        private readonly IRedisCacheService _RedisCacheService;
        private readonly IAlfrescoHelper _AlfrescoHelper;

        public PostController(IPostRepository postRepository, ILogger<PostController> logger, IRedisCacheService RedisCacheService, IAlfrescoHelper AlfrescoHelper)
        {
            _postRepository = postRepository;
            _logger = logger;
            _RedisCacheService = RedisCacheService;
            _AlfrescoHelper = AlfrescoHelper;
        }

        // GET: api/lst
        [HttpGet("List")]
        public async Task<ActionResult<IEnumerable<Post>>> ListPostAsync()
        {
            var reminders = await _postRepository.ListPostAsync();
            _logger.LogWarning("Ok - Serilog Stats 200 ListPostAsync");
            return Ok(reminders);
        }

        // GET: api/AddPost
        [HttpPost("Add")]
        public async Task<ActionResult<Post>> AddPostAsync(Post post)
        {
            try
            {
                await _postRepository.AddPostAsync(post); 
                return post;
            }
            catch (Exception ex)
            {
                _logger.LogError("AddPostAsync:", ex);
                throw;
            }

        }

        // GET: api/RedisCaheSetString
        [HttpPost("RedisGetOrSetAsync")]
        public async Task<ActionResult> RedisGetOrSetAsync(string cachekey ,string value)
        {
            try
            { 
                string result = await _RedisCacheService.GetOrSetAsync(cachekey, async () => value);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("RedisGetOrSetAsync:", ex);
                throw;
            }

        }

        //GET: api/ThreadAsync
        [HttpPost("Thread")]
        public  ActionResult ThreadAsync(int second)
        {
            try
            {
                TimeSpan interval = new TimeSpan(0, 0, second);
                Thread.Sleep(interval);
                return Ok(string.Format("Thread sleep {0}", interval));
            }
            catch (Exception ex)
            {
                _logger.LogError("ThreadAsync:", ex);
                throw;
            }
        }

        //GET: api/DeletefileName
        [HttpPost("DeleteNameFile")]
        public ActionResult AlfrescoDeleteNameFileAsync()
        {
            try
            {
                bool result =  _AlfrescoHelper.DeleteFileFromAlfresco("name");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("AlfrescoUploadAsync:", ex);
                throw;
            }

        }

    }
}
