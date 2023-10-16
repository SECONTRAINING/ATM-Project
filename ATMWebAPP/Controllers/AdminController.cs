using ATMWebAPP.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ATMWebAPP.Controllers
{
    public class AdminController : Controller
    {

        private readonly IMapper _mapper;

        List<LoginCredential> userList = new List<LoginCredential>();
        public LoginCredential loginCredential;
        public string strurl = "https://localhost:44354/api/";

        public AdminController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [Authorize]
        public IActionResult Index()
        {
            int flag = 0;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(strurl);

                    // http get


                    var responseTask = client.GetAsync("Admin/GetLogins");
                    responseTask.Wait();

                    var result = responseTask.Result;

                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<LoginCredential[]>();
                        readTask.Wait();
                        userList = readTask.Result.ToList();
                        flag = 1;

                    }
                }
            }
            catch (Exception ex) { }
            return View(userList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();

        }


        [HttpPost]
        public IActionResult Create([FromForm] LoginCredentialView login)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(strurl);

                // Build a dictionary to represent the form data
                var formData = new Dictionary<string, string>
                {
                    {"FullName",login.FullName},
                    {"UserName",login.UserName },
                    {"Email",login.Email },
                    {"Password",login.Password }
                };

                // Send the HTTP POST request with the form data
                var postTask = client.PostAsync("Admin/SaveLogin", new FormUrlEncodedContent(formData));
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

                var responseTask = client.GetAsync($"Admin/GetLoginById?Id={Id}");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<LoginCredential>();
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
        public ActionResult Edit([FromForm] LoginCredential login)
        {
            int recupdateded = 0;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(strurl);

                var formData = new Dictionary<string, string>
                {
                    {"FullName",login.FullName},
                    {"UserName",login.UserName },
                    {"Email",login.Email },
                    {"Password",login.Password }
                };

                // Send the HTTP POST request with the form data
                var postTask = client.PostAsync("Admin/UpdateLogin?LoginId=" + login.UserID, new FormUrlEncodedContent(formData));
                postTask.Wait(); // Wait for the result synchronously

                var result = postTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Update failed. Please try again.");
                }
            }

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

                var responseTask = client.GetAsync($"Admin/GetLoginById?Id={Id}");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<LoginCredential>();
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
                var responsetask = client.DeleteAsync($"Admin/DeleteLogin?LoginId={Id}");

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

        [HttpGet]
        public IActionResult Details(int Id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(strurl);

                // http get

                var responseTask = client.GetAsync($"Admin/GetLoginById?Id={Id}");
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<LoginCredential>();
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

        public List<LoginCredential> GetUserlist()
        {
            int flag = 0;
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(strurl);

                    // http get


                    var responseTask = client.GetAsync("Admin/GetLogins");
                    responseTask.Wait();

                    var result = responseTask.Result;

                    if (result.IsSuccessStatusCode)
                    {
                        var readTask = result.Content.ReadAsAsync<LoginCredential[]>();
                        readTask.Wait();
                        userList = readTask.Result.ToList();
                        flag = 1;

                    }
                }
            }
            catch (Exception ex) { }
            return userList;
        }
    }
}
