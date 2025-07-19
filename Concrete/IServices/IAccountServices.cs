using StansAssociates_Backend.Models;

namespace StansAssociates_Backend.Concrete.IServices
{
    public interface IAccountServices
    {
        Task<ServiceResponse<LoginResponse>> Login(LoginModel model);
        Task<APIResponse> ChangePassword(ChangePasswordModel model);
        Task<ServiceResponse<LoginResponse>> RefreshToken(RefreshTokenModel model);
        Task<ServiceResponse<GetProfileModel>> GetUserProfile();
        Task<ServiceResponse<GetProfileModel>> UpdateUser(UpdateProfile model);
    }
}