using DhanVatikaWeb.Models;
using DhanVatikaWeb.Service;
using Microsoft.AspNetCore.Mvc;

namespace DhanVatikaWeb.Controllers
{
    [CustomerFirstLoginFilter]
    public class DashboardController : BaseController
    {
        private readonly ApiService _apiService;
        private readonly IConfiguration configuration;
        public static string baseurl = "";

        public DashboardController(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
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

            if (propertieslist != null && propertieslist.Data.Count() > 0)
            {
                return View(propertieslist.Data);
            }
            return View();
        }

        public async Task<IActionResult> PaySlip(long plotId, long CustomerPaymentId)
        {
            var customerId = HttpContext.Session.GetString("CustomerId");
            CustomerReceiptResDto plotResponseDto = new CustomerReceiptResDto();
            var request = new CommonRequestDto<CustomerReceiptReqDto>
            {

                CompanyId = 1,
                PageRecordCount = 10,
                PageSize = 1,
                UserId = 1,
                Data = new CustomerReceiptReqDto
                {
                    CustomerId = Convert.ToInt64(customerId),
                    PlotId= plotId,
                    CustomerPaymentId= CustomerPaymentId

                }
            };
            string plotapiUrl = baseurl + "Customer/GetCustomerPaymentReceiptService";



            CommonResponseDto<CustomerReceiptResDto> paylist =
            await _apiService.SendAsync<CommonRequestDto, CommonResponseDto<CustomerReceiptResDto>>(plotapiUrl, request, "POST");
            if (paylist != null)
            {
                if (paylist.Data != null)
                {
                    return View(paylist.Data);
                }
            }
            return View();
        }
    }
}
