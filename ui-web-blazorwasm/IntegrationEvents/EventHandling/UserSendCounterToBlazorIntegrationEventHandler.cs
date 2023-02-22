
using EventBus.Abstractions;
using ui.web.blazorwasm.IntegrationEvents.Events;

namespace ui.web.blazorwasm.IntegrationEvents.EventHandling
{
    public class UserSendCounterToBlazorIntegrationEventHandler : IIntegrationEventHandler<UserSendCounterToBlazorIntegrationEvent>
    {
      
        private readonly ILogger<UserSendCounterToBlazorIntegrationEventHandler> _logger;

        public UserSendCounterToBlazorIntegrationEventHandler(ILogger<UserSendCounterToBlazorIntegrationEventHandler> logger)
        {
            _logger = logger; 
        }

        public async Task Handle(UserSendCounterToBlazorIntegrationEvent @event) 
        {
            _logger.LogInformation("----- Handling integration event: {IntegrationEventId} - ({@IntegrationEvent})", @event.randomCount, @event);
             
        }
    }
}
