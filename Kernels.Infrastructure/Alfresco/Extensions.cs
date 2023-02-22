using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kernels.Infrastructure.Alfresco
{
    public static class Extensions
    {
        public static IServiceCollection AddCustomAlfresco(this IServiceCollection services, IConfiguration config,
       Action<AlfrescoOptions>? setupAction = null)
        {

            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (services.Contains(ServiceDescriptor.Singleton<IAlfrescoHelper, AlfrescoHelper>()))
            {
                return services;
            }

            //var alfrescoOptions = new AlfrescoOptions();
            //var alfrescoSection = config.GetSection("Alfresco"); // json Alfresco is AlfrescoOptions 
            //alfrescoSection.Bind(alfrescoOptions);
            //services.Configure<AlfrescoOptions>(alfrescoSection);
            //setupAction?.Invoke(alfrescoOptions);

            services.AddSingleton<IAlfrescoHelper, AlfrescoHelper>();

            return services; 
        }
    }
}
