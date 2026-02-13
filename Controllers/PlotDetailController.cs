using DhanVatikaWeb.Models;
using DhanVatikaWeb.Service;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DhanVatikaWeb.Controllers
{
    public class PlotDetailController : Controller
    {
        private readonly ApiService _apiService;
        private readonly IConfiguration configuration;
        public static string baseurl = "";
        public PlotDetailController()
        {
            _apiService = new ApiService();
            configuration = new ConfigurationBuilder()
              .AddJsonFile("appsettings.json")
              .Build();
            baseurl = configuration["ApiBaseUrl"];
        }
        public async Task<IActionResult> Index(int plotdetailId)
        {

            string plotapiUrl = baseurl + "Web/GetPlotWebService";
            var request = new CommonRequestDto<PtotWebReq>
            {
                CompanyId = 1,
                PageRecordCount = 10,
                PageSize = 1,
                UserId = 1,
                Data= new PtotWebReq
                {
                    PLotId= plotdetailId
                }
            };
           
            CommonResponseDto<PlotResponseDto> plot =
            await _apiService.SendAsync<CommonRequestDto<PtotWebReq>, CommonResponseDto<PlotResponseDto>>(plotapiUrl, request, "POST");
            if (plot != null)
            {
                if (plot.Data != null)
                {
                    if (plot.Data.Amenities != null)
                    {
                        plot.Data.PlotAmenties = JsonConvert.DeserializeObject<List<AmentiesList>>(plot.Data.Amenities);
                    }
                }
            }
            return View(plot.Data);
        }

        [HttpPost]
        public async Task<JsonResult> SendEnquiry(EnquiryFormModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string plotapiUrl = baseurl+ "Enquiry/AddEnquiryService";
                    var request = new CommonRequestDto<EnquiryFormModel>
                    {
                        CompanyId = 1,
                        PageRecordCount = 10,
                        PageSize = 1,
                        UserId = 1,
                        Data = model
                    };


                    CommonResponseDto<ResponseDto> enquiry =
           await _apiService.SendAsync<CommonRequestDto<EnquiryFormModel>, CommonResponseDto<ResponseDto>>(plotapiUrl, request, "POST");
                    if (enquiry.Data.flag == 1)
                    {
                        return Json(new
                        {
                            success = true,
                            message = enquiry.Message
                        });
                    }
                    else
                    {
                        return Json(new
                        {
                            success = false,
                            message = enquiry.Message
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
