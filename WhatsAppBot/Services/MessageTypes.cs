using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhatsAppBot.Services
{
    public enum MessageTypes : int
    {
        ReOrder,
        OrderStatus,
        DropOffLocation,
        NewInEOL,
        Suggest,
        Feedback
    }
}