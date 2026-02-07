using DhanVatikaWeb.Models;
using DhanVatikaWeb.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace DhanVatikaWeb.Controllers
{
    [CustomerFirstLoginFilter]
    public class CustomerPlotController : Controller
    {
        private readonly ApiService _apiService;
        private readonly IConfiguration configuration;
        public static string baseurl = "";
        public CustomerPlotController()
        {
            _apiService = new ApiService();
            configuration = new ConfigurationBuilder()
              .AddJsonFile("appsettings.json")
              .Build();
            baseurl = configuration["ApiBaseUrl"];
        }
        public async Task<IActionResult> Index(int plotId)
        {
            var customerId = HttpContext.Session.GetString("CustomerId");
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
                propertieslist = new CommonResponseDto<List<PlotForCustomerResponseDto>> {
                    Data = propertieslist.Data.Where(x => x.PlotId == plotId).ToList()
                };

                return View(propertieslist.Data);
            }
            return View(null);
        }
    }
}
