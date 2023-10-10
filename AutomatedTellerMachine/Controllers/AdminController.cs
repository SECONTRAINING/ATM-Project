using Microsoft.AspNetCore.Mvc;

namespace AutomatedTellerMachine.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult AdminDetails()
        {
            return View();
        }
    }
}
