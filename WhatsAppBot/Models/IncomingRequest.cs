using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhatsAppBot.Models
{
    public class IncomingRequest
    {
        public string sid { get; set; }
        public string date_created { get; set; }
        public string date_updated { get; set; }
        public object date_sent { get; set; }
        public string account_sid { get; set; }
        public string to { get; set; }
        public string from { get; set; }
        public object messaging_service_sid { get; set; }
        public string body { get; set; }
        public string status { get; set; }
        public string num_segments { get; set; }
        public string num_media { get; set; }
        public string direction { get; set; }
        public string api_version { get; set; }
        public object price { get; set; }
        public object price_unit { get; set; }
        public object error_code { get; set; }
        public object error_message { get; set; }
        public string uri { get; set; }
        public SubresourceUris subresource_uris { get; set; }
    }

    public class SubresourceUris
    {
        public string media { get; set; }
    }
}