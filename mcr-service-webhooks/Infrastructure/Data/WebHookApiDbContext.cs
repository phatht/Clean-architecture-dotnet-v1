using mcr_service_webhooks.Domain.Events;
using mcr_service_webhooks.Domain.Models;
using Microsoft.EntityFrameworkCore;


namespace mcr_service_webhooks.Infrastructure.Data
{

    public class WebHookApiDbContext : DbContext
    {

        public DbSet<WebHook> WebHooks { get; set; }

        public DbSet<WebHookRecord> WebHooksHistory { get; set; }

        public DbSet<DomainEvent> Events { get; set; }

        public WebHookApiDbContext(
            DbContextOptions<WebHookApiDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(WebHookApiDbContext).Assembly);

            modelBuilder.Entity<WebHookCreated>().ToTable("WebHookCreatedEvent");

            modelBuilder.Entity<WebHookRemoved>().ToTable("WebHookRemovedEvent");

            modelBuilder.Entity<WebHookUpdated>().ToTable("WebHookUpdatedEvent");

            //modelBuilder.Entity<WebHook>().HasData(
            //    new WebHook()
            //    {
            //        ID = 1,
            //        WebHookUrl = "https://localhost:5015/hookloopback",
            //        IsActive = true,
            //        ContentType = "application/json",
            //        HookEvents = new HookEventType[] { HookEventType.hook }
            //    });

            base.OnModelCreating(modelBuilder);
        }

       
    }
}
