using mcr_service_webhooks.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace mcr_service_webhooks.Infrastructure.Data
{
    public class WebHookConfiguration
   : IEntityTypeConfiguration<WebHook>
    {
        public void Configure(EntityTypeBuilder<WebHook> builder)
        {

            builder.HasKey(e => e.ID);

            builder.HasMany(e => e.Headers)
            .WithOne(e => e.WebHook)
            .HasForeignKey(e => e.WebHookID);

            builder.HasMany(e => e.Records)
            .WithOne(e => e.WebHook)
            .HasForeignKey(e => e.WebHookID);
        }
    }
}
