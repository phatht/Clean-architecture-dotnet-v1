using EventBus.Abstractions;
using mcr_service_post.Domain.Interfaces;
using mcr_service_post.Domain.Models;
using mcr_service_post.Infrastructure.IntegrationEvents.Events;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace mcr_service_post.Infrastructure.IntegrationEvents.EventHandling
{
    
    public class UserMessIntegrationEventHandler : IIntegrationEventHandler<UserMessIntegrationEvent>
    {
        private readonly IPostRepository _postRepository;
        private readonly ILogger<UserMessIntegrationEventHandler> _logger;

        public UserMessIntegrationEventHandler(IPostRepository postRepository, ILogger<UserMessIntegrationEventHandler> logger)
        {
            _postRepository = postRepository;
            _logger = logger;

        }

        public async Task Handle(UserMessIntegrationEvent @event)
        {
            _logger.LogInformation("----- Handling integration event: {IntegrationEventId} - ({@IntegrationEvent})", @event.UserId, @event);

            Post p = new Post(); 
            await _postRepository.AddPostAsync(p.NewPost(@event.UserId));

        }
    }
}
