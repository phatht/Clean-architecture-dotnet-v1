using EventBus.Abstractions;
using mcr_service_user.Domain.Interfaces;
using mcr_service_user.Domain.Models;
using mcr_service_user.Infrastructure.IntegrationEvents.Events;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;


namespace mcr_service_user.Infrastructure.IntegrationEvents.EventHandling
{
    public class UserMessIntegrationEventHandler : IIntegrationEventHandler<UserMessIntegrationEvent>
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserMessIntegrationEventHandler> _logger;

        public UserMessIntegrationEventHandler(IUserRepository userRepository, ILogger<UserMessIntegrationEventHandler> logger)
        {
            _userRepository = userRepository;
            _logger = logger;

        }

        public async Task Handle(UserMessIntegrationEvent @event)
        {
            _logger.LogInformation("----- Handling integration event: {IntegrationEventId} - ({@IntegrationEvent})", @event.UserId, @event);
             
            User u = new User(); 
            await _userRepository.AddAsync(u.NewUser(@event.UserId));

        }
    }
}
