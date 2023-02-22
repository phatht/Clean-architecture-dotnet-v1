using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mcr_service_user.Domain.Models
{
    public enum UserStatus
    {
        Active,
        Delete,
        Lock,
    }
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Mail { get; set; }
        public string OtherData { get; set; } 
        public UserStatus Status { get; set; } = UserStatus.Active;

        public User NewUser(Guid id)
        {
            return new User
            {
                Id = id, 
                Name = "New events rabbitMq",
                Mail = "phatht@vietinfo.tech",
                Status = UserStatus.Lock, 
            };
        }
    }

    
}
