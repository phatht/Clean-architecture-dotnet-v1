﻿using System;
using System.Collections.Generic;

namespace mcr_service_webhooks.Domain.Models
{
    public class WebHook
    {

        public WebHook()
        {
            this.Headers = new HashSet<WebHookHeader>();
            this.HookEvents = HookEventType.hook;
            this.Records = new List<WebHookRecord>();
        }

        /// <summary>
        /// Hook DB Id
        /// </summary>
        public long ID { get; set; }

        /// <summary>

        /// <summary>
        /// Webhook endpoint
        /// </summary>
        public string WebHookUrl { get; set; }

        /// <summary>
        /// Webhook secret
        /// </summary>
#nullable enable
        public string? Secret { get; set; }
#nullable disable

        /// <summary>
        /// Content Type
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// Is active / NotActiv
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Hook Events context
        /// </summary>
        public HookEventType HookEvents { get; set; }

        /// <summary>
        /// Additional HTTP headers. Will be sent with hook.
        /// </summary>
        virtual public HashSet<WebHookHeader> Headers { get; set; }

        /// <summary>
        /// Hook call records history
        /// </summary>
        virtual public ICollection<WebHookRecord> Records { get; set; }

        /// <summary>
        /// Timestamp of last hook trigger
        /// </summary>
        /// <value></value>
        public DateTime? LastTrigger { get; set; }
    }
}
