using System;

namespace mcr_service_webhooks.Domain.Models
{
    public class DomainEvent
    {
        public long ID { get; set; }
 
        public Guid? ActorID { get; set; }
 
        public DateTime TimeStamp { get; set; }

        public EventType EventType { get; set; }
    }

    public enum EventType
    {
        WebHook,
        System,
        Project,
    }
}
