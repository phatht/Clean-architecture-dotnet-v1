using EventBus.Events;
using System;

namespace mcr_service_user.Infrastructure.IntegrationEvents.Events
{
    public record UserSendCounterToBlazorIntegrationEvent : IntegrationEvent
    {
        public long randomCount { get; set; }

        public UserSendCounterToBlazorIntegrationEvent(long randomCount)
        =>
            this.randomCount = randomCount;
        
    }
}
