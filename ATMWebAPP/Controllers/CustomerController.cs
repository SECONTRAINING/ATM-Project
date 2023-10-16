using ATMWebAPP.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;

namespace ATMWebAPP.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IMapper _mapper;

        List<CustomerAccount> customerList = new List<CustomerAccount>();
        public CustomerAccount customer;
        public string strurl = "https://localhost:44354/api/";

        public CustomerController(IMapper mapper)
        {
            _mapper = mapper;
        }

        public IActionResult Index()
        {

            int flag = 0;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(strurl);

                    // http get


                    var responseTask = client.GetAsync("Account/GetCustomers");
                    responseTask.Wait();

                    var result = responseTask.Result;

                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<CustomerAccount[]>();
                        readTask.Wait();
                        customerList = readTask.Result.ToList();
                        flag = 1;

                    }
                }
            }
            catch (Exception ex) { }
            return View(customerList);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();

        }


        [HttpPost]
        public IActionResult Create([FromForm] CustomerAccountView ctmView)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(strurl);

                // Build a dictionary to represent the form data
                var formData = new Dictionary<string, string>
                {
                    { "FullName", ctmView.FullName },
                    { "FathersName", ctmView.FathersName },
                    { "DOB",ctmView.DOB.ToString("yyyy-MM-dd")},
                    { "Gender",ctmView.Gender},
                    { "PermanentAddress",ctmView.PermanentAddress},
                    { "PresentAddress",ctmView.PresentAddress},
                    { "PANNumber",ctmView.PANNumber},
                    { "MobileNumber",ctmView.MobileNumber},
                    { "Landline",ctmView.Landline},
                    { "AccountType",ctmView.AccountType},
                    { "OpeningBalance",ctmView.OpeningBalance.ToString()},
                    { "AccNo",ctmView.AccNo.ToString()},
                    { "CardNo",ctmView.CardNo.ToString()},
                    { "PIN",ctmView.PIN.ToString()},
                    { "CheckBook",ctmView.BranchID.ToString()},
                    { "BranchID",ctmView.BranchID.ToString()},
                };

                // Send the HTTP POST request with the form data
                var postTask = client.PostAsync("Account/SaveUser", new FormUrlEncodedContent(formData));
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<int>();
                    readTask.Wait();

                    var insertedGrade = readTask.Result;
                    if (insertedGrade > 0)
                    {
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    return View();
                }
            }

            return View();
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            // call the web api GetGragdeById method 
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(strurl);

                // http get

                var responseTask = client.GetAsync($"Account/GetUserById?userId={Id}");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<CustomerAccount>();
                    readTask.Wait();

                    var std = readTask.Result;
                    if (std != null)
                    {
                        ViewData["CustomerId"] = Id;
                        return View(std);
                    }

                }
                else
                {
                    Console.WriteLine("Can't retrieve record...!");
                }

            }
            return View();

        }


        [HttpPost]
        public ActionResult Edit([FromForm] CustomerAccount updatedAccount)
        {
            int recupdateded = 0;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(strurl);

                var formData = new Dictionary<string, string>
                {
                    { "FullName", updatedAccount.FullName },
                    { "FathersName", updatedAccount.FathersName },
                    { "DOB",updatedAccount.DOB.ToString("yyyy-MM-dd")},
                    { "Gender",updatedAccount.Gender},
                    { "PermanentAddress",updatedAccount.PermanentAddress},
                    { "PresentAddress",updatedAccount.PresentAddress},
                    { "PANNumber",updatedAccount.PANNumber},
                    { "MobileNumber",updatedAccount.MobileNumber},
                    { "Landline",updatedAccount.Landline},
                    { "AccountType",updatedAccount.AccountType},
                    { "OpeningBalance",updatedAccount.OpeningBalance.ToString()},
                    { "AccNo",updatedAccount.AccNo.ToString()},
                    { "CardNo",updatedAccount.CardNo.ToString()},
                    { "PIN",updatedAccount.PIN.ToString()},
                    { "CheckBook",updatedAccount.BranchID.ToString()},
                    { "BranchID",updatedAccount.BranchID.ToString()},
                };

                // Send the HTTP POST request with the form data
                var postTask = client.PostAsync("Account/UpdateCustomer?userId=" + updatedAccount.CustomerID, new FormUrlEncodedContent(formData));
                postTask.Wait(); // Wait for the result synchronously

                var result = postTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    // If the update is successful, you can handle it accordingly
                    // For example, you can redirect to the updated record's details page
                    //return RedirectToAction("Details", new { Id = updatedAccount.Id });
                    return RedirectToAction("Index");
                }
                else
                {
                    // Handle the case where the update fails, e.g., display an error message
                    ModelState.AddModelError(string.Empty, "Update failed. Please try again.");
                }
            }

            // If there's an error or the update fails, return to the edit view with the updated data
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int Id)
        {
            // call the web api GetGragdeById method 
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(strurl);

                // http get

                var responseTask = client.GetAsync($"Account/GetUserById?userId={Id}");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<CustomerAccount>();
                    readTask.Wait();

                    var std = readTask.Result;
                    if (std != null)
                    {

                        return View(std);
                    }

                }
                else
                {
                    Console.WriteLine("Can't retrieve record...!");
                }

            }
            return View();

        }

        [HttpPost]
        public ActionResult DeletePost(int Id)
        {
            int recDeleted = 0;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(strurl);
                var responsetask = client.DeleteAsync($"Account/DeleteCustomerAccount?CustomerAccountId={Id}");

                responsetask.Wait();

                var result = responsetask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readtask = result.Content.ReadAsAsync<int>();
                    readtask.Wait();

                    recDeleted = readtask.Result;

                    if (recDeleted > 0)
                    {
                        return RedirectToAction("Index");

                    }

                }
            }
            return View();

        }

    }
    
}
