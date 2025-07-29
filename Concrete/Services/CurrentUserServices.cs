using Newtonsoft.Json;
using StansAssociates_Backend.Concrete.IServices;
using System.Security.Claims;

namespace StansAssociates_Backend.Concrete.Services
{
    public class CurrentUserServices : ICurrentUserServices
    {
        private readonly IHttpContextAccessor _httpContext;

        public CurrentUserServices(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }


        public bool IsAuthenticated => _httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) != null;

        public int UserId => Convert.ToInt32(_httpContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

        public string Email => _httpContext.HttpContext.User.FindFirst(ClaimTypes.Email).Value;

        public string Name => _httpContext.HttpContext.User.FindFirst(ClaimTypes.Name).Value;

        public string PhoneNumber => _httpContext.HttpContext.User.FindFirst(ClaimTypes.OtherPhone).Value;

        public string Pincode => _httpContext.HttpContext?.User?.FindFirst(ClaimTypes.PostalCode)?.Value;

        public string RoleType => _httpContext.HttpContext.User.FindFirst(ClaimTypes.Role).Value;

        public List<int> RoleIds => JsonConvert.DeserializeObject<List<int>>(_httpContext.HttpContext.User.FindFirst("role_ids").Value);

        public bool IsAdmin => RoleType.Equals("Admin", StringComparison.OrdinalIgnoreCase) || RoleIds.Contains(1);

    }
}
