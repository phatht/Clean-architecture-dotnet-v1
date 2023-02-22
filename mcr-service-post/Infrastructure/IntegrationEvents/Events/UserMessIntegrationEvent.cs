using EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mcr_service_post.Infrastructure.IntegrationEvents.Events
{
    public record UserMessIntegrationEvent : IntegrationEvent
    {
        public Guid UserId { get; set; }

        public UserMessIntegrationEvent(Guid userId)
        =>
            UserId = userId;

    }
}
