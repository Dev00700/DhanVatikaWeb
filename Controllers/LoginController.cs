using DhanVatikaWeb.Models;
using DhanVatikaWeb.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net;
using static System.Net.WebRequestMethods;

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

        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task< IActionResult> ForgotPassword(ForgotPassword obj)
        {
            ForgotPassword _req = new ForgotPassword();
            _req.Email = obj.Email;
            var request = new CommonRequestDto<ForgotPassword>
            {
                CompanyId = 1,
                PageRecordCount = 10,
                PageSize = 1,
                UserId = 1,
                Data = _req

            };
          
            string apiUrl = baseurl + "Web/CheckCusmtomerEmailService";

            try
            {
                CommonResponseDto<ForgotPasswordResponse> res =
                await _apiService.SendAsync<CommonRequestDto, CommonResponseDto<ForgotPasswordResponse>>(apiUrl, request, "POST");

                if (res.Data.Flag == 0)
                {
                    ViewBag.Flag = res.Data.Flag;
                    ViewBag.Message = res.Data.Message;
                }
                else
                {
                   
                    string encryptedEmail = CryptoHelper.Encrypt(obj.Email);
                    string encodedEmail = WebUtility.UrlEncode(encryptedEmail);
                    string resetUrl = $"{Request.Scheme}://{Request.Host}/SetPassword/ValidateOtp?e={encodedEmail}";

                    var emailService = new EmailService(configuration);
                    emailService.SendMail(
                        obj.Email,
                        "Change Your Password",
                        ChangePasswordTemplate(resetUrl, res.Data.OTP)
                    );

                    ViewBag.Flag = res.Data.Flag;
                    ViewBag.Message = res.Data.Message;
                }
                
                }catch  (Exception ex)
            {
                // Handle exception (e.g., log it)
               
            }

            return View();
        }

        public static string ChangePasswordTemplate(string link,string otp)
        {
            return $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset='UTF-8'>
</head>
<body style='font-family:Segoe UI; background:#f4f6fb; padding:20px;'>
    <div style='max-width:600px; margin:auto; background:#ffffff; padding:25px; border-radius:8px;'>

        <h2 style='color:#333;'>Password Reset Request</h2>

        <p style='color:#555; font-size:14px;'>
            We received a request to reset your password.
            Please use the OTP below to verify your request.
        </p>

        <div style='text-align:center; margin:20px 0;'>
            <span style='font-size:24px;
                         letter-spacing:6px;
                         font-weight:bold;
                         color:#667eea;
                         border:1px dashed #667eea;
                         padding:10px 20px;
                         display:inline-block;'>
                {otp}
            </span>
        </div>

        <p style='font-size:13px; color:#777;'>
            This OTP is valid for <b>10 minutes</b>.
        </p>

        <div style='text-align:center; margin:25px 0;'>
            <a href='{link}'
               style='padding:12px 25px;
                      background:#667eea;
                      color:#ffffff;
                      text-decoration:none;
                      border-radius:5px;
                      font-size:14px;'>
                Change Password
            </a>
        </div>

        <p style='font-size:12px; color:#999;'>
            If you did not request a password reset, please ignore this email.
        </p>

    </div>
</body>
</html>";
        }
    }
}
