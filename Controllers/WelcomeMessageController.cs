using Microsoft.AspNetCore.Mvc;

namespace PaymentCalculator.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class WelcomeMessageController
    {
        [HttpGet]
        public ActionResult<string> Welcome()
        {
            return "Hello TAP!";
        }
    }
}
