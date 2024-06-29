using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OTPApi.Services;
using Otp_Generate_With_Verification.Models;

namespace OTPApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OtpController : ControllerBase
    {

        private readonly IOTPService _otpService;

        public OtpController(IOTPService otpService)
        {
            _otpService = otpService;
        }

        [HttpPost("generate")]
        public IActionResult GenerateOTP([FromBody] GenerateOTPRequest request)
        {
            var otp = _otpService.GenerateOTP(request.UserIdentifier);
            // Ideally, send OTP to the user via email/SMS (implementation not shown here)
            return Ok(new { OTP = otp });
        }

        [HttpPost("verify")]
        public IActionResult VerifyOTP([FromBody] VerifyOTPRequest request)
        {
            var isVerified = _otpService.VerifyOTP(request.UserIdentifier, request.OTP);
            if (isVerified)
            {
                return Ok(new { Message = "OTP verified successfully" });
            }
            else
            {
                return BadRequest(new { Message = "Invalid OTP" });
            }
        }
    }
}
