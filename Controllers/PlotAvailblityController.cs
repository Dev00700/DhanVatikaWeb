using DhanVatikaWeb.Models;
using DhanVatikaWeb.Service;
using Microsoft.AspNetCore.Mvc;

namespace DhanVatikaWeb.Controllers
{
    public class PlotAvailblityController : Controller
    {
        private readonly ApiService _apiService;
        private readonly IConfiguration configuration;
        public static string baseurl = "";
        public PlotAvailblityController()
        {
            _apiService = new ApiService();
            configuration = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json")
               .Build();
            baseurl = configuration["ApiBaseUrl"];
        }
        public async Task<IActionResult> Index(int LocationId,int status)
        {
            ViewBag.LocationId = LocationId;
            ViewBag.Status = status;
            PlotResponseDto plotResponseDto = new PlotResponseDto();
            var request = new CommonRequestDto<PtotWebReq>
            {
                CompanyId = 1,
                PageRecordCount = 10,
                PageSize = 1,
                UserId = 1,
                Data= new PtotWebReq
                {
                    LocationId= LocationId,
                    Status= status
                }
            };
            string plotapiUrl = baseurl + "Web/GetPlotWebListService";

          

            CommonResponseDto<List<PlotResponseDto>> plot =
            await _apiService.SendAsync<CommonRequestDto, CommonResponseDto<List<PlotResponseDto>>>(plotapiUrl, request, "POST");

            var locationrequest = new CommonRequestDto
            {
                CompanyId = 1,
                PageRecordCount = 10,
                PageSize = 1,
                UserId = 1,
            };
            string locationapirul = baseurl + "Web/GetLocationListService";



            CommonResponseDto<List<LocationDto>> locations =
            await _apiService.SendAsync<CommonRequestDto, CommonResponseDto<List<LocationDto>>>(locationapirul, locationrequest, "POST");
            if (locations != null && locations.Data.Count() > 0)
            {
                ViewBag.LocationList = locations.Data;
            }

            if (plot != null && plot.Data.Count() >0)
            {
                return View(plot.Data);
            }
           
            return View(null);
        }
    }
}
