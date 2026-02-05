using DhanVatikaWeb.Models;
using DhanVatikaWeb.Service;
using Microsoft.AspNetCore.Mvc;

namespace DhanVatikaWeb.Controllers
{
    public class ChangePasswordController : BaseController
    {
        private readonly ApiService _apiService;
        private readonly IConfiguration configuration;
        public static string baseurl = "";
        public ChangePasswordController(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
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

        [HttpPost]
        public async Task<JsonResult> ChangePassword(UpdatePasswordReqDto model)
        {
            var customerGuid = HttpContext.Session.GetString("CustomerGuid");
            if (string.IsNullOrEmpty(customerGuid))
                return Json(new { success = false, message = "Session expired. Please login again." });

            if (model.NewPassword != model.ConfirmPassword)
                return Json(new { success = false, message = "New password and confirmation do not match." });

            string apiUrl = baseurl + "Customer/UpdateCustomerPasswordService";
            model.CustomerGuid = Guid.Parse(customerGuid);

            var request = new CommonRequestDto<UpdatePasswordReqDto>
            {
                CompanyId = 1,
                PageRecordCount = 10,
                PageSize = 1,
                UserId = 1,
                Data = model
            };

            CommonResponseDto<UpdatePasswordResponseDto> response =
                await _apiService.SendAsync<CommonRequestDto<UpdatePasswordReqDto>, CommonResponseDto<UpdatePasswordResponseDto>>(apiUrl, request, "POST");
            if (response.Data != null)
            {
                if (response.Data.flag == 1)
                    return Json(new { success = true, message = "Password changed successfully!" });
                if (response.Data.flag == 0)
                {
                    return Json(new { success = false, message = response.Data.message });
                }
            }
                return Json(new { success = false, message = response.Message });
        }
    }
}
