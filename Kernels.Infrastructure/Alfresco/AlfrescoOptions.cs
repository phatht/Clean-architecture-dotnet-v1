using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kernels.Infrastructure.Alfresco
{
    public class AlfrescoOptions
    {
        public string ServerPath { get; set; } = "";
        public string Host { get; set; } = "";
        public string Port { get; set; } = "";
        public string HostFTP { get; set; } = "";
        public string Prefix { get; set; } = "";
        public string Username { get; set; } = "";
        public string Password { get; set; } = ""; 
        public int MaxFileSizeMB { get; set; }
        public long MaxFileSize { get; set; }
    }
}
