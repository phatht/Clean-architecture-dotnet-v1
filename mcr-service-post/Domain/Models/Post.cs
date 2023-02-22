using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mcr_service_post.Domain.Models
{
    
    public class Post
    {
        public Guid Id { get; set; }
        public string Tile { get; set; }
        public string Content { get; set; }     
        public Guid UserId { get; set; }
        public string Name { get; set; }

        public Post NewPost(Guid id)
        {
            return new Post
            {
                Id = id,
                Tile = "Tile new",
                Content = "Content new",
                UserId = Guid.NewGuid(),
                Name = "new from rabbitMQ"
            };
        }
    }


    
}
