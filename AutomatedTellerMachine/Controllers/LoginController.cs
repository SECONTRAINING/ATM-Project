using Microsoft.AspNetCore.Mvc;

namespace AutomatedTellerMachine.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult AdminDetails()
        {
            return View();
        }
    }
}
