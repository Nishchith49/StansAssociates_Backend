using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace StansAssociates_Backend.Models
{
    public class RegisterModel : EmailModel
    {
        [JsonProperty("userName")]
        public string UserName { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("role")]
        public int Role { get; set; }

        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; }
    }


    public class EmailModel
    {
        [JsonProperty("email")]
        public string Email { get; set; }
    }


    public class VerifyOtpModel : EmailModel
    {
        [JsonProperty("otp")]
        public string OTP { get; set; }

        [JsonProperty("roleId")]
        public long RoleId { get; set; } = 2;
    }


    public class LoginModel : EmailModel
    {
        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("roleId")]
        public long RoleId { get; set; } = 2;
    }


    public class LoginResponse
    {
        [JsonProperty("accessToken")]
        public string AccessToken { get; set; }

        [JsonProperty("expiresIn")]
        public int ExpiresIn { get; set; }

        [JsonProperty("refreshToken")]
        public string RefreshToken { get; set; }

        [JsonProperty("roles")]
        public List<string> Roles { get; set; }

        [JsonProperty("tokenType")]
        public string TokenType { get; set; }

        [JsonProperty("emailId")]
        public string EmailId { get; set; }

        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; }

        [JsonProperty("userName")]
        public string UserName { get; set; }
    }


    public class RefreshTokenModel
    {
        [JsonProperty("refreshToken")]
        public string RefreshToken { get; set; }
    }


    public class GetEmail
    {
        [JsonProperty("email")]
        [Required]
        //[RegularExpression("[A-Za-z0-9]+@[a-z]+.[a-z]{2,3}", ErrorMessage = "please enter a valid email")]
        public string Email { get; set; }
    }


    public class ForgotPasswordModel : GetEmail
    {
        [JsonProperty("roleId")]
        public long RoleId { get; set; } = 2;
    }


    public class ChangePasswordModel
    {
        [JsonProperty("currentPassword")]
        public string CurrentPassword { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }


    public class ResetPasswordModel
    {
        [JsonProperty("email")]
        [Required]
        [RegularExpression("[A-Za-z0-9]+@[a-z]+.[a-z]{2,3}", ErrorMessage = "please enter a valid email")]
        public string Email { get; set; }

        [JsonProperty("token")]
        [Required]
        public string Token { get; set; }

        [JsonProperty("password")]
        [Required]
        public string Password { get; set; }

        [JsonProperty("roleId")]
        public long RoleId { get; set; } = 2;
    }


    public class InitiateLoginModel
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("countryCode")]
        public string CountryCode { get; set; }

        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; }
    }


    public class ValidateLoginOtpModel : InitiateLoginModel
    {
        [JsonProperty("otp")]
        public int Otp { get; set; }
    }


    public class GetProfileModel : UpdateProfile
    {
        [JsonProperty("userId")]
        public long UserId { get; set; }

        [JsonProperty("isPasswordSet")]
        public bool IsPasswordSet { get; set; }
    }


    public class UpdateProfile
    {
        [JsonProperty("userName")]
        public string UserName { get; set; }

        [JsonProperty("emailId")]
        public string EmailId { get; set; }

        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("dob")]
        public DateTime DOB { get; set; }

        [JsonProperty("street")]
        public string Street { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("pincode")]
        public string Pincode { get; set; }

        [JsonProperty("profilePicture")]
        public string ProfilePicture { get; set; }
    }
}
