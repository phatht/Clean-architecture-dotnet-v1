using mcr_service_webhooks.Domain.Models;

namespace mcr_service_webhooks.Domain.Events
{
    public class WebHookCreated : DomainEvent
    {
        public WebHookCreated() { }

        public long WebHookId { get; set; }

        // Add any custom props hire...
    }
}
