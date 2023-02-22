using EventBus.Events;
using System;

namespace mcr_service_user.Infrastructure.IntegrationEvents.Events
{
    public record UserMessIntegrationEvent : IntegrationEvent
    {
        public Guid UserId { get; set; }

        public UserMessIntegrationEvent(Guid userId)
        =>
            UserId = userId;
        
    }
}
