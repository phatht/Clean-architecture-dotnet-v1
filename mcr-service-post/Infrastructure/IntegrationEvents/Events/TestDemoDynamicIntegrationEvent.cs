using EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mcr_service_post.Infrastructure.IntegrationEvents.Events
{
    public record TestDemoDynamicIntegrationEvent 
    {
        public string id { get; set; }
        public string fullName { get; set; }

        public TestDemoDynamicIntegrationEvent(string id, string fullName)
        {
            this.id = id;
            this.fullName = fullName;
        }

    }
}
