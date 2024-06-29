namespace Otp_Generate_With_Verification.Models
{
    public class GenerateOTPRequest
    {
        public required string UserIdentifier { get; set; }
    }

    public class VerifyOTPRequest
    {
        public required string UserIdentifier { get; set; }
        public required string OTP { get; set; }
    }
}
