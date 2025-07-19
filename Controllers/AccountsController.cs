using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StansAssociates_Backend.Concrete.IServices;
using StansAssociates_Backend.Global;
using StansAssociates_Backend.Models;

namespace StansAssociates_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountServices _accountServices;

        public AccountsController(IAccountServices accountServices) 
        {
            _accountServices = accountServices;
        }


        [HttpPost("[action]")]
        public async Task<ServiceResponse<LoginResponse>> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
                return new(ResponseConstants.InvalidInput, 400);
            return await _accountServices.Login(model);
        }


        [HttpPost("[action]"), Authorize]
        public async Task<APIResponse> ChangePassword(ChangePasswordModel model)
        {
            if (!ModelState.IsValid)
                return new(ResponseConstants.InvalidInput, 400);
            return await _accountServices.ChangePassword(model);
        }


        [HttpPost("[action]")]
        public async Task<ServiceResponse<LoginResponse>> RefreshToken(RefreshTokenModel model)
        {
            if (!ModelState.IsValid)
                return new(ResponseConstants.InvalidInput, 400);
            return await _accountServices.RefreshToken(model);
        }


        [HttpGet("[action]")]
        [Authorize]
        public async Task<ServiceResponse<GetProfileModel>> GetUserProfile()
        {
            if (!ModelState.IsValid)
                return new(ResponseConstants.InvalidInput, 400);
            return await _accountServices.GetUserProfile();
        }


        [HttpPost("[action]")]
        [Authorize]
        public async Task<ServiceResponse<GetProfileModel>> UpdateUser(UpdateProfile model)
        {
            if (!ModelState.IsValid)
                return new(ResponseConstants.InvalidInput, 400);
            return await _accountServices.UpdateUser(model);
        }
    }
}
