using Microsoft.AspNetCore.Mvc;
using PaymentCalculator.Services;

namespace PaymentCalculator.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class ResetCountController
    {
        private IHitCounterService HitCounterService;

        public ResetCountController(IHitCounterService hitCounterService)
        {
            HitCounterService = hitCounterService;
        }

        [HttpGet]
        public ActionResult<string> ResetCount()
        {
            HitCounterService.Reset();
            return "OK";
        }
    }
}
