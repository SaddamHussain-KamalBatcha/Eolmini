using System.Web.Mvc;
using Twilio;
using Twilio.AspNet.Mvc;
using Twilio.TwiML;
using WhatsAppBot.Models;
using WhatsAppBot.Services;

namespace WhatsAppBot.Controllers
{
    public class HomeController : TwilioController
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("incoming")]
        public TwiMLResult Index(IncomingRequest request)
        {
            Authentication();

            var eolMiniService = new EolMiniService();

            var result = eolMiniService.GetIncomingRequest(request);

            var response = new MessagingResponse();

            response.Message(
                            result.body,
                            result.to,
                            result.from,
                            null,
                            null,
                            null);

            return TwiML(response);
        }

        private static void Authentication()
        {
            var accountSid = "ACf3bd3016394a47eccc3868dec8b6fa7d";
            var authToken = "1d0d9bf3c2948e884e65bb621dbb7f5d";
            TwilioClient.Init(accountSid, authToken);
        }
    }
}