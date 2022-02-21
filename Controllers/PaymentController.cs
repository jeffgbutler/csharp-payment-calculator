using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PaymentCalculator.Models;
using PaymentCalculator.Services;


namespace PaymentCalculator.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class PaymentController
    {
        private PaymentService PaymentService;
        private IHitCounterService HitCounterService;

        private readonly ILogger _logger;

        public PaymentController(PaymentService paymentService,
                IHitCounterService hitCounterService,
                ILogger<PaymentController> logger)
        {
            PaymentService = paymentService;
            HitCounterService = hitCounterService;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<CalculatedPayment> calculatePayment(double Amount, double Rate, int Years)
        {
            var Payment = PaymentService.Calculate(Amount, Rate, Years);

            _logger.LogDebug("Calculated payment of {Payment} for input amount: {Amount}, rate: {Rate}, years: {Years}",
                Payment, Amount, Rate, Years);

            return new CalculatedPayment
            {
                Amount = Amount,
                Rate = Rate,
                Years = Years,
                Instance = "local",
                Count = HitCounterService.GetAndIncrement(),
                Payment = Payment
            };
        }
    }
}
