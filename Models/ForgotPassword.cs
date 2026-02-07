namespace DhanVatikaWeb.Models
{
    public class ForgotPassword
    {
      public string Email { get; set; }
    }
    public class ForgotPasswordResponse
    {
        public int Flag { get; set; }
        public string Message { get; set; }
        public string OTP { get; set; }
    }
    public class PasswordChange
    {
        public string Email { get; set; }
        public string? ConfirmPassword { get; set; }
        public string NewPassword { get; set; }
    }
    public class ValidateOTP
    {
        public string Email { get; set; }
        public string OTP { get; set; }
       
    }

    public class ValidateOTPResponse
    {
        public string Email { get; set; }
        public int Flag { get; set; }
        public string Message { get; set; }

    }
    public class PasswordChangeF
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
