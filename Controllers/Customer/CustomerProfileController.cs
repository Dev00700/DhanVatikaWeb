using DhanVatikaWeb.Models;
using DhanVatikaWeb.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace DhanVatikaWeb.Controllers.Customer
{
    public class CustomerProfileController : BaseController
    {
        private readonly ApiService _apiService;
        private readonly IConfiguration configuration;
        public static string baseurl = "";
        public CustomerProfileController(IHttpContextAccessor httpContextAccessor):base(httpContextAccessor)
        {
            _apiService = new ApiService();
            configuration = new ConfigurationBuilder()
              .AddJsonFile("appsettings.json")
              .Build();
            baseurl = configuration["ApiBaseUrl"];
        }
        public async Task<IActionResult> Index()
        {
            var customerId = HttpContext.Session.GetString("CustomerId");
            PlotForCustomerResponseDto plotResponseDto = new PlotForCustomerResponseDto();
            var request = new CommonRequestDto<PlotForCustomerRequestDto>
            {

                CompanyId = 1,
                PageRecordCount = 10,
                PageSize = 1,
                UserId = 1,
                Data = new PlotForCustomerRequestDto
                {
                    CustomerId = Convert.ToInt32(customerId)
                }
            };
            string plotapiUrl = baseurl + "Customer/CustomerWisePlotService";


         
            CommonResponseDto<List<PlotForCustomerResponseDto>> propertieslist =
            await _apiService.SendAsync<CommonRequestDto, CommonResponseDto<List<PlotForCustomerResponseDto>>>(plotapiUrl, request, "POST");

            if (propertieslist.Data.Count() > 0)
            {
                return View(propertieslist.Data);
            }
            return View(null);
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
            model.CustomerGuid =Guid.Parse(customerGuid);

            var request = new CommonRequestDto<UpdatePasswordReqDto>
            {
                CompanyId = 1,
                PageRecordCount = 10,
                PageSize = 1,
                UserId = 1,
                Data = model
            };

            CommonResponseDto<object> response =
                await _apiService.SendAsync<CommonRequestDto<UpdatePasswordReqDto>, CommonResponseDto<object>>(apiUrl, request, "POST");

            if (response.Flag == 1)
                return Json(new { success = true, message = "Password changed successfully!" });

            return Json(new { success = false, message = response.Message });
        }
    }
}
