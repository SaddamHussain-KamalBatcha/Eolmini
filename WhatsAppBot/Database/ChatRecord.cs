using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WhatsAppBot.Database
{
    public class ChatRecord
    {
        [Key]
        public int Id { get; set; }
        public string MobileNumber { get; set; }
        public string LastSentMessage { get; set; }
        public string LastSentFormat { get; set; }
        public string LastReceivedMessage { get; set; }
        public DateTime SentOn { get; set; }
    }
}