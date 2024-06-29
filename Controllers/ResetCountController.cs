using Microsoft.AspNetCore.Mvc;
using Services;

namespace Controllers;

[Route("/[controller]")]
[ApiController]
public class ResetCountController(IHitCounterService hitCounterService)
{
    [HttpGet]
    public ActionResult<string> ResetCount()
    {
        hitCounterService.Reset();
        return "OK";
    }
}
