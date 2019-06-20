using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhatsAppBot.Models
{
    public enum OrderingStep
    {
        OrderDetails = 1,
        SampleDetails = 2,
        Analyses = 3,
        Review = 4,
        Confirmation = 5
    }
}