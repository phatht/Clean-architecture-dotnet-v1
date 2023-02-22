using mcr_service_user.Domain.Exceptions;
using mcr_service_user.Domain.Interfaces;
using mcr_service_user.Domain.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace mcr_service_user.Domain.Services
{
    
    public class UserService : IUserRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<UserService> _logger;

        public UserService(IUnitOfWork unitOfWork, ILogger<UserService> logger)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<User>> ListUserAsync()
        {
            return await _unitOfWork.UserRepository.ListUserAsync();
        }


        public async Task<User> AddAsync(User user)
        {
            try
            {
                var result = await _unitOfWork.UserRepository.AddAsync(user);
                await _unitOfWork.CommitAsync(); 
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error when create user {ex}", ex.Message);
                throw;
            }
        }


    }
}
