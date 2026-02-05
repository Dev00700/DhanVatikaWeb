using DhanVatikaWeb.Models;
using DhanVatikaWeb.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace DhanVatikaWeb.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApiService _apiService;
        private readonly IConfiguration configuration;
        public static string baseurl = "";
        public LoginController()
        {
            _apiService = new ApiService();
            configuration = new ConfigurationBuilder()
              .AddJsonFile("appsettings.json")
              .Build();
            baseurl = configuration["ApiBaseUrl"];
        }
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("", "Login");
        }


        [HttpPost]
        public async Task<JsonResult> CustomerLogin(LoginRequestDto model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string loginurl = baseurl + "Customer/CustomerLoginService";
                    var request = new CommonRequestDto<LoginRequestDto>
                    {
                        CompanyId = 1,
                        PageRecordCount = 10,
                        PageSize = 1,
                        UserId = 1,
                        Data = model
                    };


                    CommonResponseDto<LoginResponseDto> login =
           await _apiService.SendAsync<CommonRequestDto<LoginRequestDto>, CommonResponseDto<LoginResponseDto>>(loginurl, request, "POST");

                    if (login.Data != null)
                    {
                        if (login.Data.CustomerGuid != null)
                        {
                            HttpContext.Session.SetString("CustomerGuid", login.Data.CustomerGuid.ToString() ?? "");
                            HttpContext.Session.SetString("CustomerId", login.Data.CustomerId.ToString() ?? "");
                            HttpContext.Session.SetString("CustomerName", login.Data.Name ?? "");
                            HttpContext.Session.SetString("CustomerEmail", login.Data.EmailId ?? "");
                            HttpContext.Session.SetString("CustomerMobile", login.Data.Mobile ?? "");
                            HttpContext.Session.SetString("IsFirstLogin", login.Data.IsFirstLogin.ToString() ?? "");
                            if(login.Data.IsFirstLogin==true)
                            {
                                return Json(new
                                {
                                    success = true,
                                    message = "Login Successfully",
                                    redirectUrl = Url.Action("Index", "ChangePassword")
                                });
                            }
                            else
                            {
                                return Json(new
                                {
                                    success = true,
                                    message = "Login Successfully",
                                    redirectUrl = Url.Action("Index", "Dashboard")
                                });
                            }
                               
                        }
                        return Json(new
                        {
                            success = false,
                            message = login.Data.message
                        });
                    }
                    else
                    {
                        return Json(new
                        {
                            success = false,
                            message = login.Data.message
                        });
                    }

                }
                catch (Exception ex)
                {
                    return Json(new
                    {
                        success = false,
                        message = "Error saving data: " + ex.Message
                    });
                }
            }

            // ❌ Validation failed
            return Json(new
            {
                success = false,
                message = "Please fill all required fields correctly."
            });
        }
    }
}
