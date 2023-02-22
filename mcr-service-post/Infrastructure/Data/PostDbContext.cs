using mcr_service_post.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace mcr_service_user.Infrastructure.Data
{
    public class PostDbContext : DbContext
    {
        public DbSet<Post> Post { get; set; } 
         
        public PostDbContext(DbContextOptions<PostDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Fluent API
            modelBuilder.Entity<Post>(ConfigurePost);
           
            modelBuilder.Entity<Post>().HasData(
                new Post
                {
                    Id = Guid.NewGuid(),
                    Tile = "Tile 1",
                    Content = "Content 1", 
                    UserId = Guid.NewGuid(),
                    Name = "Huỳnh Tấn Phát" 
                },
                 new Post
                 {
                     Id = Guid.NewGuid(),
                     Tile = "Tile 2",
                     Content = "Content 2",
                     UserId = Guid.NewGuid(),
                     Name = "Huỳnh Tấn Phát"
                 }
            );
        }

        private void ConfigurePost(EntityTypeBuilder<Post> entity)
        {
            entity.ToTable("Posts");
            entity.HasKey(r => r.Id); 
            entity.Property(r => r.Content).IsRequired(); 
        }
         
    }
}
