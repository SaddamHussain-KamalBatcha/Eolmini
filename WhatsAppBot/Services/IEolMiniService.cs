using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatsAppBot.Models;

namespace WhatsAppBot.Services
{
    public interface IEolMiniService
    {
        void GetIncomingRequest(IncomingRequest request);
    }
}
