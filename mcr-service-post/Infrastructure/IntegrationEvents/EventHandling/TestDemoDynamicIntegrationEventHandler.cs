using EventBus.Abstractions;
using mcr_service_post.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace mcr_service_post.Infrastructure.IntegrationEvents.EventHandling
{
    public class TestDemoDynamicIntegrationEventHandler : IDynamicIntegrationEventHandler
    {
        private readonly IPostRepository _postRepository;
        private readonly ILogger<TestDemoDynamicIntegrationEventHandler> _logger;

        public TestDemoDynamicIntegrationEventHandler(IPostRepository postRepository, ILogger<TestDemoDynamicIntegrationEventHandler> logger)
        {
            _postRepository = postRepository;
            _logger = logger;

        }

        public async Task Handle(dynamic eventData) // dynamic eventData
        { 
            var jsonResponse = System.Text.Json.JsonSerializer.Serialize(eventData);
            if (jsonResponse != null)
            { 
            }      
        }
    }
}
