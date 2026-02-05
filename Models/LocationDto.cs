namespace DhanVatikaWeb.Models
{
    public class HomeWebResponseDto
    {
        public IEnumerable<LocationDto> Locations { get; set; }
        public IEnumerable<PlotResponseDto> Plots { get; set; }
    }

    public class LocationDto
    {
        public Guid LocationGuid { get; set; }
        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public string? Image { get; set; }
        public int? TotalPlot { get; set; }
    }

  

    public class PtotWebReq
    {
        public int LocationId { get; set; }
        public int PLotId { get; set; }

    }

    public class PlotResponseDto
    {
        public Guid PlotGuid { get; set; }
        public long PlotId { get; set; }
        public string PlotCode { get; set; }
        public string Plot_Code { get; set; }
        public string SubPlotCode { get; set; }
        public string PlotName { get; set; }
        public string Description { get; set; }
        public int LocationId { get; set; }
        public string Address { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public decimal AreaSize { get; set; }
        public int UnitTypeId { get; set; }
        public decimal Price { get; set; }
        public int Status { get; set; }
        public string Facing { get; set; }
        public string PlotType { get; set; }
        public string NearbyLandmarks { get; set; }
        public string PlotStatus { get; set; }


        public string? LocationName { get; set; }
        public string? UnitTypeName { get; set; }
        public bool? IsShowONWeb { get; set; }
        public string? Amenities { get; set; }
        public string? Image { get; set; }
        public List<PlotImageDto>? PlotImage { get; set; }
        public List<AmentiesList>? PlotAmenties { get; set; }
        public List<LocationDto>? LocationList { get; set; }

    }

    public class PlotImageDto
    {
        public Guid PlotImageGuid { get; set; }
        public int PlotId { get; set; }
        public string Image { get; set; }

    }
    public class AmentiesList
    {
        public string amentyname { get; set; }
        public string amentydescr { get; set; }
    }

    public class EnquiryFormModel
    {
        public int PlotId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string? Remarks { get; set; }
    }

    public class ResponseDto
    {
        public int flag { get; set; }
        public string message { get; set; }

    }

    public class LoginRequestDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class LoginResponseDto : ResponseDto
    {
        public Guid? CustomerGuid { get; set; }
        public long? CustomerId { get; set; }
        public long EnquiryId { get; set; }
        public string Name { get; set; }
        public string EmailId { get; set; }
        public string Mobile { get; set; }
        public string Remarks { get; set; }
        public string? Image { get; set; }
        public string? Password { get; set; }
        public bool? IsFirstLogin { get; set; }
    }

    public class UpdatePasswordReqDto
    {
        public Guid CustomerGuid { get; set; }
        public string? OldPassword { get; set; }
        public string? ConfirmPassword { get; set; }
        public string NewPassword { get; set; }
    }

    public class PlotForCustomerResponseDto
    {
        public Guid PlotGuid { get; set; }
        public long PlotId { get; set; }
        public string PlotCode { get; set; }
        public string SubPlotCode { get; set; }
        public string Plot_Code { get; set; }
        public string PlotName { get; set; }
        public string Description { get; set; }
        public int LocationId { get; set; }
        public string Address { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public decimal AreaSize { get; set; }
        public int UnitTypeId { get; set; }
        public decimal Price { get; set; }
        public int Status { get; set; }
        public string Facing { get; set; }
        public string PlotType { get; set; }
        public string NearbyLandmarks { get; set; }
        public string PlotStatus { get; set; }
        public string? LocationName { get; set; }
        public string? UnitTypeName { get; set; }
        public bool? IsShowONWeb { get; set; }
        public string? Amenities { get; set; }
        public string? Image { get; set; }
        public decimal? Amount { get; set; }
        public string? Remarks { get; set; }
        public List<PlotImageDto>? PlotImage { get; set; }
        public List<CustomerPlotPaymentDto>? CustomerPlotPaymentList { get; set; }
        public Guid? PlotImageGuid { get; set; }
        public DateTime? paymentDate { get; set; }
      


    }

    public class CustomerPlotPaymentDto
    {
        public long customerpaymentid { get; set; }
        public int EmiNo { get; set; }
        public decimal amount { get; set; }
        public decimal paidamount { get; set; }
        public DateTime emidate { get; set; }
        public bool ispaid { get; set; }
        public string remarks { get; set; }
        public decimal DueAmount { get; set; }
        public decimal PreviousDue { get; set; }
        public decimal TotalPendingAmount { get; set; }
        public bool? ReceiptFlag { get; set; }
        public string? NewRemarks { get; set; }
        public bool? IsRejected { get; set; }
    }


    public class PlotForCustomerRequestDto
    {
        public long CustomerId
        {
            get; set;
        }
    }

    public class UpdatePasswordResponseDto : ResponseDto
    {
    
    }

    public class CustomerReceiptReqDto
    {
        public long CustomerId { get; set; }
        public long PlotId { get; set; }
        public long CustomerPaymentId { get; set; }
    }

    public class CustomerReceiptResDto
    {
        public int EmiNo { get; set; }
        public decimal Amount { get; set; }
        public decimal PaidAmount { get; set; }
        public DateTime EMIDate { get; set; }
        public DateTime PaidDate { get; set; }
        public string CustomerName { get; set; }
        public string PlotCode { get; set; }
        public string Plot_Code { get; set; }
        public string SubPlotCode { get; set; }
        public string PlotName { get; set; }
        public string Mobile { get; set; }
        public string Address { get; set; }
    }
}
