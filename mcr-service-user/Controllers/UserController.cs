using EventBus.Abstractions;
using mcr_service_user.Domain.Interfaces;
using mcr_service_user.Domain.Models;
using mcr_service_user.Infrastructure.IntegrationEvents.Events;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace mcr_service_user.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IEventBus _eventBus;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserRepository userRepository, IEventBus eventBus, ILogger<UserController> logger)
        { 
            _userRepository = userRepository;
            _eventBus = eventBus;
            _logger = logger;
        }

        // GET: api/Users
        [HttpGet("List")] 
        public async Task<ActionResult<IEnumerable<User>>> ListUserAsync()
        {
            var users = await _userRepository.ListUserAsync();
            _logger.LogWarning("Ok - Serilog Stats 200");
            return Ok(users);
        }

        // POST: api/User
        [HttpPost("Add")] 
        public async Task<ActionResult<User>> AddAsync(User user)
        {
            await _userRepository.AddAsync(user);
            //return CreatedAtAction("ListUserAsync", new { id = user.Id }, user);
            return user;



        }



        //POST: api/RabbitMQ
        [HttpPost("RabbitMQ")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<string>> RabbitMQAsync()
        {
            try
            {
                //var users = await _userRepository.ListUserAsync();
                //var id = Guid.NewGuid();
                //if (string.IsNullOrEmpty(id.ToString()))
                //{
                //    return BadRequest();
                //}

                //var eventMessage = new UserMessIntegrationEvent(id);
                //_eventBus.Publish(eventMessage);

                Random random = new Random();
                long randomCount = random.Next(1000);

                var eventMessage = new UserSendCounterToBlazorIntegrationEvent(randomCount);
                _eventBus.Publish(eventMessage);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ERROR Publishing integration event"); 
                throw;
            }

            return Accepted();
        }

        // GET: api/Users
        [HttpGet("WriteSerilog")]
        public ActionResult WriteSerilog()
        {
            _logger.LogWarning("LogWarning - WriteSerilog to server .235");
            _logger.LogError("LogError - WriteSerilog to server .235");
            _logger.LogInformation("LogInformation - WriteSerilog to server .235"); 
            return Ok("Ghi log lên server Seq thành công !");
        }
    }
}
