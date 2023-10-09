using BusinessLogicLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AutomatedTellerMachine.APILogs
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class APIAccountController : ControllerBase
    {
        private readonly BLLForCustomerAccount _bLLForCustomerAccount;
        public APIAccountController(BLLForCustomerAccount bLLForCustomerAccount)
        {
            _bLLForCustomerAccount = bLLForCustomerAccount;
        }

        [HttpGet]
        [Route("api/Account/GetCustomers")]
        public List<CustomerAccountView> GetCustomers()
        {
            try
            {
                return _bLLForCustomerAccount.GetCustomerAccounts();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        [Route("api/Account/SaveUser")]
        public int SaveUser(CustomerAccountView userView)
        {
            int irecsaved;
            try
            {
                irecsaved = _bLLForCustomerAccount.SaveCustomerAccount(userView);
            }
            catch (Exception)
            {

                throw;
            }
            return irecsaved;
        }

        [HttpGet]
        [Route("api/Account/GetUserById")]
        public CustomerAccountView GetUserById(int userId)
        {
            try
            {
                return _bLLForCustomerAccount.GetCustomerAccountById(userId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        [Route("api/Account/UpdateCustomer")]
        public int UpdateCustomer(int userId, CustomerAccountView userView)
        {
            try
            {
                return _bLLForCustomerAccount.UpdateCustomerAccount(userId, userView);
            }

            catch (Exception ex)
            {
                // Handle other exceptions
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete]
        [Route("api/Account/DeleteCustomerAccount")]
        public int DeleteCustomerAccount(int CustomerAccountId)
        {
            try
            {
                return _bLLForCustomerAccount.DeleteCustomerAccount(CustomerAccountId);
            }
            catch (Exception ex)
            {
                // Handle exceptions or log them as needed
                throw new Exception(ex.Message);
            }
            return 1;
        }
    }
}
