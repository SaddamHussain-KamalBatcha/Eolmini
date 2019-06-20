using System.Web.Http;
using Twilio;
using WhatsAppBot.Models;
using WhatsAppBot.Services;

namespace WhatsAppBot.Controllers
{
    [RoutePrefix("api/eolmini")]
    public class EolMiniController : ApiController
    {

        [HttpGet]
        public IHttpActionResult Index()
        {
            return Ok("WelCome");
        }

        [HttpPost]
        [Route("incoming")]
        public IHttpActionResult Index(IncomingRequest request)
        {
            Authentication();

            var eolMiniService = new EolMiniService();
            var result = eolMiniService.GetIncomingRequest(request);
            return Ok(result);
        }

        private static void Authentication()
        {
            var accountSid = "ACf3bd3016394a47eccc3868dec8b6fa7d";
            var authToken = "1d0d9bf3c2948e884e65bb621dbb7f5d";
            TwilioClient.Init(accountSid, authToken);
        }
    }
}
