using mcr_service_webhooks.Domain.Models;

namespace mcr_service_webhooks.Domain.Events
{
    public class WebHookRemoved : DomainEvent
    {
        public WebHookRemoved() { }

        public long WebHookId { get; set; }

        // Add any custom props hire...
    }
     
}
