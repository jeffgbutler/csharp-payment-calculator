using Microsoft.AspNetCore.Mvc;
using Models;
using Services;

namespace Controllers;

[Route("/[controller]")]
[ApiController]
public class PaymentController(
    IHitCounterService hitCounterService,
    ILogger<PaymentController> logger)
{
    [HttpGet]
    public ActionResult<CalculatedPayment> CalculatePayment(double amount, double rate, int years)
    {
        var payment = PaymentService.Calculate(amount, rate, years);

        logger.LogDebug("Calculated payment of {Payment} for input amount: {Amount}, rate: {Rate}, years: {Years}",
            payment, amount, rate, years);

        return new CalculatedPayment
        {
            Amount = amount,
            Rate = rate,
            Years = years,
            Instance = "local",
            Count = hitCounterService.GetAndIncrement(),
            Payment = payment
        };
    }
}
