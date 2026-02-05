using DhanVatikaWeb.Models;
using DhanVatikaWeb.Service;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DhanVatikaWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApiService _apiService;
        private readonly IConfiguration configuration;
        public static string baseurl = "";
        public HomeController()
        {
            _apiService = new ApiService();
            configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            baseurl= configuration["ApiBaseUrl"];
        }

        public async Task<IActionResult> Index()
        {
            HomeWebResponseDto response = new HomeWebResponseDto();
           var request = new CommonRequestDto
            {
                CompanyId = 1,
                PageRecordCount = 10,
                PageSize = 1,
                UserId = 1,
            };
            string apiUrl = baseurl + "Web/GetLocationListService";
            string plotapiUrl = baseurl + "Web/GetPlotWebHomeService";

            CommonResponseDto<List<LocationDto>> location =
            await _apiService.SendAsync<CommonRequestDto, CommonResponseDto<List<LocationDto>>>(apiUrl, request, "POST");

            CommonResponseDto<List<PlotResponseDto>> plot =
            await _apiService.SendAsync<CommonRequestDto, CommonResponseDto<List<PlotResponseDto>>>(plotapiUrl, request, "POST");

            if (location.Data != null)
            {
                response.Locations = location.Data;
            }
            if (plot.Data != null)
            {
                response.Plots = plot.Data;
            }
            return View(response);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
