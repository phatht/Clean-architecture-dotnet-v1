using EventBus.Events;

namespace ui.web.blazorwasm.IntegrationEvents.Events
{
    public record UserSendCounterToBlazorIntegrationEvent : IntegrationEvent
    {
        public long randomCount { get; set; }

        public UserSendCounterToBlazorIntegrationEvent(long randomCount)
        =>
            this.randomCount = randomCount;
    }
}
