namespace StansAssociates_Backend.Global
{
    public static class ResponseConstants
    {
        public const string InvalidInput = "Invalid input";
        public const string Success = "Success";
        public const string Failed = "Failed";
        public const string InvalidId = "Invalid Id";
        public const string AlreadyExists = "Already Exists";


        public const string UserExists = "User Already Exists";
        public const string EmailExists = "Email Already Exists";
        public const string PhoneExists = "Phone Number Already Exists";
        public const string UserNotExists = "User Does Not Exists";
        public const string InvalidEmail = "Please enter the valid email";
        public const string InvalidPassword = "Please enter the valid password";
        public const string LoginFail = "Invalid Credentials";
        public const string OTPSent = "OTP has been sent successfully";
        public const string ContactAdmin = "Login Failed, Please Contact Admin";
        public const string InvalidOtp = "The OTP is expired or wrong";
        public const string LoginSuccess = "SuccessFully Logged-in";
        public const string DuplicateOtp = "Verification Failed, Please try resend OTP.";
        public const string NotVerified = "Please Verify Your Account";
        public const string SomeUpdatesFailed = "Some Objects not updated";




        public const string ShiprocketAuthFailed = "Failed to authenticate with shiprocket, please contact admin";
        public const string ShiprocketFailed = "Shiprocket API failed";


    }

    public static class S3Directories
    {
        public const string StaticMedia = "StaticMedia";
        public const string ProfileMedia = "ProfileMedia";
        public const string StudentMedia = "StudentMedia";
    }


    public static class TemplateName
    {
        public const string Otp = "otp.html";
        public const string EmailChange = "otp.html";
        public const string ForgotPassword = "ForgotPassword.html";
    }

    public static class TemplateReplaceStrings
    {
        public const string ReplaceOTP = "{OTP}";
        public const string UserName = "{userName}";
        public const string Password = "{password}";
    }

    public static class EmailSubject
    {
        public const string OtpSubject = "OTP to login";
    }
}
