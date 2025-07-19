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

        [JsonProperty("referralCode")]
        public string ReferralCode { get; set; }

        public AddAddressModel Address { get; set; }
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


    public class GetProfileModel
    {
        [JsonProperty("userId")]
        public long UserId { get; set; }

        [JsonProperty("userName")]
        public string UserName { get; set; }

        [JsonProperty("emailId")]
        public string EmailId { get; set; }

        [JsonProperty("mobileNumber")]
        public string MobileNumber { get; set; }
    }


    public class UpdateProfile
    {
        [JsonProperty("userName")]
        public string UserName { get; set; }

        [JsonProperty("emailId")]
        public string EmailId { get; set; }
    }


    public class UpdateEmailModel
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("oldOtp")]
        public string OldOtp { get; set; }

        [JsonProperty("newOtp")]
        public string NewOtp { get; set; }
    }

    public class UpdatePhoneNumberModel
    {
        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; }

        [JsonProperty("oldOtp")]
        public string OldOtp { get; set; }

        [JsonProperty("newOtp")]
        public string NewOtp { get; set; }
    }

    public class UpdateAddressModel : AddAddressModel
    {
        [JsonProperty("addressId")]
        public long AddressId { get; set; }
    }


    public class GetAddressModel : UpdateAddressModel
    {
        [JsonProperty("cityName")]
        public string CityName { get; set; }

        [JsonProperty("stateName")]
        public string StateName { get; set; }

        [JsonProperty("countryName")]
        public string CountryName { get; set; }
    }


    public class AddAddressModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("address1")]
        public string Address1 { get; set; }

        [JsonProperty("address2")]
        public string Address2 { get; set; }

        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; }

        [JsonProperty("pincode")]
        public string Pincode { get; set; }

        [JsonProperty("cityId")]
        public string CityId { get; set; }

        [JsonProperty("stateId")]
        public string StateId { get; set; }

        [JsonProperty("countryId")]
        public string CountryId { get; set; }

        [JsonProperty("isDefault")]
        public bool IsDefault { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("latitude")]
        public double? Latitude { get; set; }

        [JsonProperty("longitude")]
        public double? Longitude { get; set; }
    }


    public class LocationData
    {
        public string FormattedAddress { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Pincode { get; set; }

        public long CountryId { get; set; }
        public long StateId { get; set; }
        public long CityId { get; set; }
        public long SubDistrictId { get; set; }

        public bool IsValid() => !string.IsNullOrEmpty(FormattedAddress) &&
                                 !string.IsNullOrEmpty(Country) &&
                                 !string.IsNullOrEmpty(State) &&
                                 !string.IsNullOrEmpty(City) &&
                                 !string.IsNullOrEmpty(Pincode);
    }
}
