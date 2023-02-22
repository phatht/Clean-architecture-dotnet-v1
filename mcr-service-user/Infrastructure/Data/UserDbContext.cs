using mcr_service_user.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace mcr_service_user.Infrastructure.Data
{
    public class UserDbContext : DbContext
    {
        public DbSet<User> User { get; set; }

        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Fluent API
            modelBuilder.Entity<User>(ConfigureUser);

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = Guid.NewGuid(),
                    Name = "Huỳnh Tấn Phát",
                    Mail = "phatht@vietinfo.tech",
                    Status = UserStatus.Active,

                },
                new User
                {
                    Id = Guid.NewGuid(),
                    Name = "Phát",
                    Mail = "phat@gmail.com",
                    Status = UserStatus.Lock,
                }

            );
        }

        private void ConfigureUser(EntityTypeBuilder<User> entity)
        {
            entity.ToTable("Users");
            entity.HasKey(r => r.Id);
            entity.Property(r => r.Name).IsRequired();
            entity.Property(r => r.Status).HasColumnType("varchar(150)").IsRequired();
        }
    }
}
