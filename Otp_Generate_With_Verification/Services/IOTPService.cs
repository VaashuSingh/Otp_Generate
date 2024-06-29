using System.Collections.Concurrent;

namespace OTPApi.Services
{
    public interface IOTPService
    {
        string GenerateOTP(string userIdentifier);
        bool VerifyOTP(string userIdentifier, string otp);
    }

    public class OTPService : IOTPService
    {
        private readonly ConcurrentDictionary<string, string> _otpStore = new();
        private readonly Random _random = new();

        public string GenerateOTP(string userIdentifier)
        {
            string otp = _random.Next(100000, 999999).ToString();
            _otpStore[userIdentifier] = otp;
            return otp;
        }

        public bool VerifyOTP(string userIdentifier, string otp)
        {
            if (_otpStore.TryGetValue(userIdentifier, out var storedOtp))
            {
                if (storedOtp == otp)
                {
                    _otpStore.TryRemove(userIdentifier, out _); // OTP should be used only once
                    return true;
                }
            }
            return false;
        }
    }
}
