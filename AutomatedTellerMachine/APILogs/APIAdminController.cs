using BusinessLogicLayer;
using DataObjectLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AutomatedTellerMachine.APILogs
{
    //[Route("api/[controller]")]
    //[ApiController]
    public class APIAdminController : ControllerBase
    {
        private readonly BLLForAdmin _bLLForAdmin;
        public APIAdminController(BLLForAdmin bLLForAdmin)
        {
            _bLLForAdmin = bLLForAdmin;
        }

        [HttpGet]
        [Route("api/Admin/GetLogins")]
        public List<LoginCredentialView> GetLogins()
        {
            try
            {
                return _bLLForAdmin.GetLogins();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        [Route("api/Admin/SaveLogin")]
        public int SaveLogin(LoginCredentialView loginCredential)
        {
            try
            {
               return _bLLForAdmin.SaveLogin(loginCredential);

            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        [Route("api/Admin/GetLoginById")]
        public LoginCredentialView GetLoginById(int Id)
        {
            try
            {
                return _bLLForAdmin.GetLoginById(Id);
            }

            catch (Exception ex)
            {
                // Handle other exceptions
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        [Route("api/Admin/UpdateLogin")]
        public int UpdateLogin(int LoginId, LoginCredentialView loginCredential)
        {
            try
            {
                return _bLLForAdmin.UpdateLogin(LoginId, loginCredential);
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete]
        [Route("api/Admin/DeleteLogin")]
        public int DeleteLogin(int LoginId)
        {
            try
            {
                return _bLLForAdmin.DeleteLogin(LoginId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
