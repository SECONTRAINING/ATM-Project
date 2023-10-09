using AutomatedTellerMachine.APILogs;
using BusinessLogicLayer;
using Microsoft.AspNetCore.Mvc;

namespace AutomatedTellerMachine.Controllers
{
    public class CustomerController : Controller
    {
        private readonly BLLForCustomerAccount bLLForCustomerAccount;
        public CustomerController(BLLForCustomerAccount bLLForCustomerAccount)
        {
            this.bLLForCustomerAccount = bLLForCustomerAccount;
        }
        public IActionResult Customers()
        {
            return View(bLLForCustomerAccount.GetCustomerAccounts());
        }
    }
}
