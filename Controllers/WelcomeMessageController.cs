using Microsoft.AspNetCore.Mvc;

namespace Controllers;

[Route("/[controller]")]
[ApiController]
public class WelcomeMessageController
{
    [HttpGet]
    public ActionResult<string> Welcome()
    {
        return "Hello TP4K8S!";
    }
}
