using DhanVatikaWeb.Models;
using DhanVatikaWeb.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Web;

namespace DhanVatikaWeb.Controllers
{
 
    public class SetPasswordController : Controller
    {
        private readonly ApiService _apiService;
        private readonly IConfiguration configuration;
        public static string baseurl = "";

        public SetPasswordController()
        {
            _apiService = new ApiService();
            configuration = new ConfigurationBuilder()
              .AddJsonFile("appsettings.json")
              .Build();
            baseurl = configuration["ApiBaseUrl"];
        }
        [HttpGet]
        public IActionResult Index(string e)
        {
            if (string.IsNullOrEmpty(e))
                return RedirectToAction("Login");

            //string decryptedEmail = CryptoHelper.Decrypt(WebUtility.UrlDecode(e));
            string decryptedEmail = WebUtility.UrlDecode(e);

            ViewBag.EmailId = decryptedEmail;
            PasswordChange obj = new PasswordChange();
            obj.Email = decryptedEmail;
            return View(obj);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task< IActionResult> Index(PasswordChange obj)
        {
            if (obj.NewPassword != obj.ConfirmPassword)
            {
                ViewBag.Message = "Passwords do not match";
                ViewBag.MessageType = "error";
                return View();
            }
            PasswordChangeF _req =new PasswordChangeF();
            _req.Email = obj.Email;
            _req.Password = obj.NewPassword;
            var request = new CommonRequestDto<PasswordChangeF>
            {
                CompanyId = 1,
                PageRecordCount = 10,
                PageSize = 1,
                UserId = 1,
                Data = _req

            };

            string apiUrl = baseurl + "Web/PasswordChangeService";
            CommonResponseDto<ValidateOTPResponse> res =
                await _apiService.SendAsync<CommonRequestDto, CommonResponseDto<ValidateOTPResponse>>(apiUrl, request, "POST");
            if (res.Data.Flag == 1)
            {
                ViewBag.Flag = res.Data.Flag;
                ViewBag.Message = res.Data.Message;
            }
            else
            {
                ViewBag.Flag = res.Data.Flag;
                ViewBag.Message = res.Data.Message;
            }
                return View();
        }
        [HttpGet]
        public IActionResult ValidateOtp(string e)
        {
            if (string.IsNullOrEmpty(e))
                return RedirectToAction("Login");

            //string decryptedEmail = CryptoHelper.Decrypt(HttpUtility.UrlDecode(e));
            string decryptedEmail = HttpUtility.UrlDecode(e);

            ViewBag.EmailId = decryptedEmail;
            ValidateOTP obj = new ValidateOTP();
            obj.Email = decryptedEmail;
            return View(obj);


        }
        [HttpPost]
        public async Task< IActionResult> ValidateOtp(ValidateOTP obj)
        {
            ValidateOTP _req = new ValidateOTP();
            _req.Email = obj.Email;
            _req.OTP = obj.OTP;
            var request = new CommonRequestDto<ValidateOTP>
            {
                CompanyId = 1,
                PageRecordCount = 10,
                PageSize = 1,
                UserId = 1,
                Data = _req

            };

            string apiUrl = baseurl + "Web/ValidateOtpService";
            CommonResponseDto<ValidateOTPResponse> res =
                await _apiService.SendAsync<CommonRequestDto, CommonResponseDto<ValidateOTPResponse>>(apiUrl, request, "POST");

            if (res.Data.Flag == 1)
            {
                ViewBag.Flag = res.Data.Flag;
                ViewBag.Message = res.Data.Message;
                //return RedirectToAction(
                //                    "Index",
                //                    "SetPassword",
                //                    new { e = CryptoHelper.Encrypt(obj.Email) });

                return RedirectToAction(
                                    "Index",
                                    "SetPassword",
                                    new { e = obj.Email });
            }
            else
            {
                ViewBag.Flag = res.Data.Flag;
                ViewBag.Message = res.Data.Message;
            }

                return View();


        }
    }
}
