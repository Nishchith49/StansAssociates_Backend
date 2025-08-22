using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using StansAssociates_Backend.Concrete.IServices;
using StansAssociates_Backend.Entities;
using StansAssociates_Backend.Global;
using StansAssociates_Backend.Models;
using System.Diagnostics.Metrics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace StansAssociates_Backend.Concrete.Services
{
    public class AccountServices : EncryptionMethods, IAccountServices
    {
        private readonly IConfiguration _configuration;
        private readonly StansassociatesAntonyContext _context;
        private readonly ICurrentUserServices _currentUser;
        private readonly IStorageServices _storageServices;

        public AccountServices(IConfiguration configuration,
                               StansassociatesAntonyContext context,
                               IStorageServices storageServices,
                               ICurrentUserServices currentUser)
        {
            _configuration = configuration;
            _context = context;
            _storageServices = storageServices;
            _currentUser = currentUser;
        }


        public async Task<ServiceResponse<LoginResponse>> Login(LoginModel model)
        {
            var user = await _context.Users
                                     .Include(x => x.UserRoles)
                                      .ThenInclude(x => x.Role)
                                     .Include(x => x.TeamPermissions)
                                     .Where(x => x.EmailId
                                                  .ToLower()
                                                  .Replace(" ", string.Empty)
                                                  .Equals(model.Email
                                                               .ToLower()
                                                               .Replace(" ", string.Empty)))
                                     .FirstOrDefaultAsync();
            if (user == null)
                return new(ResponseConstants.UserNotExists, 401);
            if (!user.IsActive)
                return new(ResponseConstants.ContactAdmin, 403);
            if (user.Password == Encipher(model.Password))
            {
                var loginResponse = GetLoginResponse(user);
                await _context.SaveChangesAsync();
                return loginResponse;
            }
            return new(ResponseConstants.InvalidPassword, 400);
        }

        private ServiceResponse<LoginResponse> GetLoginResponse(User user)
        {
            user.RefreshTokens.Add(GenerateRefreshToken());
            _context.Update(user);

            var permissions = new List<GetTeamPermissions>();

            if (user != null && user.TeamPermissions != null)
            {
                permissions = _context.Modules
                                      .Select(x => new GetTeamPermissions
                                      {
                                          ModuleId = x.Id,
                                          ModuleName = x.Name
                                      })
                                      .ToList();

                permissions.ForEach(x =>
                {
                    x.CanView = user.TeamPermissions.Where(a => a.ModuleId == x.ModuleId).Select(a => a.CanView).FirstOrDefault();
                    x.CanEdit = user.TeamPermissions.Where(a => a.ModuleId == x.ModuleId).Select(a => a.CanEdit).FirstOrDefault();
                    x.CanAdd = user.TeamPermissions.Where(a => a.ModuleId == x.ModuleId).Select(a => a.CanAdd).FirstOrDefault();
                    x.CanDelete = user.TeamPermissions.Where(a => a.ModuleId == x.ModuleId).Select(a => a.CanDelete).FirstOrDefault();
                });
            }

            var response = new LoginResponse
            {
                AccessToken = AccessToken(user, user.UserRoles.Select(x => x.Role.Name).ToList(), user.UserRoles.Select(x => (int)x.RoleId).ToList()),
                ExpiresIn = 3600,
                RefreshToken = user.RefreshTokens.Select(x => x.Token).FirstOrDefault(),
                Roles = user.UserRoles.Select(x => x.Role.Name).ToList(),
                TokenType = "Bearer",
                EmailId = user.EmailId,
                UserName = $"{user.Name}",
                PhoneNumber = user.PhoneNumber,
                ProfilePicture = user.ProfilePicture != null ? Convert.ToBase64String(user.ProfilePicture) : null,
                Permissions = permissions
            };
            return new(ResponseConstants.LoginSuccess, 200, response);
        }

        protected static RefreshToken GenerateRefreshToken()
        {
            // Type or member is obsolete
#pragma warning disable SYSLIB0023 // Type or member is obsolete
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
#pragma warning restore SYSLIB0023 // Type or member is obsolete
            // Type or member is obsolete
            var randomBytes = new byte[64];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            return new RefreshToken
            {
                Token = Convert.ToBase64String(randomBytes),
                Expires = DateTime.Now.AddDays(7),
                CreatedDate = DateTime.Now,
            };
        }

        protected string AccessToken(User appUser, List<string> roles, List<int> roleIds)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("JwtOptions:SecurityKey"));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _configuration.GetValue<string>("JwtOptions:Issuer"),
                Audience = _configuration.GetValue<string>("JwtOptions:Audience"),
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new(ClaimTypes.Email, appUser.EmailId ?? string.Empty),
                    new(ClaimTypes.Name, appUser.Name ?? string.Empty),
                    new(ClaimTypes.NameIdentifier, $"{appUser.Id}"),
                    new(ClaimTypes.OtherPhone,appUser.PhoneNumber ?? string.Empty),
                    new("roles", JsonConvert.SerializeObject(roles)),
                    new("role_ids", JsonConvert.SerializeObject(roleIds)),
                    new("school_id", JsonConvert.SerializeObject(appUser.SchoolId))
                }),
                Expires = DateTime.Now.AddYears(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            roles.ForEach(x => tokenDescriptor.Subject.AddClaim(new Claim(ClaimTypes.Role, x)));
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


        public async Task<APIResponse> ChangePassword(ChangePasswordModel model)
        {
            var appUser = await _context.Users.Where(x => x.Id == _currentUser.UserId).FirstOrDefaultAsync();
            if (model.Password == model.CurrentPassword)
            {
                return new("New password is same as current password", 400);
            }
            if (appUser is not null && (Decipher(appUser.Password) == model.CurrentPassword))
            {
                appUser.Password = Encipher(model.Password);
                appUser.UpdatedDate = DateTime.Now;
                _context.Entry(appUser).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return new APIResponse("password has been changed successfully", 200);
            }
            else
            {
                return new APIResponse("please enter a valid password", 400);
            }
        }


        public async Task<ServiceResponse<LoginResponse>> RefreshToken(RefreshTokenModel model)
        {
            var token = await _context.RefreshTokens.Where(x => x.Token == model.RefreshToken).FirstOrDefaultAsync();
            if (token is not null && DateTime.Now <= token.Expires)
            {
                var user = await _context.Users.Where(x => x.Id == token.UserId)
                                               .Include(x => x.UserRoles)
                                                .ThenInclude(x => x.Role)
                                               .FirstOrDefaultAsync();
                List<string> roles = user.UserRoles.Select(x => x.Role.Name).ToList();
                var rt = GenerateRefreshToken();
                user.RefreshTokens.Add(rt);
                _context.Remove(token);
                await _context.SaveChangesAsync();

                var res = new LoginResponse
                {
                    AccessToken = AccessToken(user, roles, user.UserRoles.Select(x => (int)x.RoleId).ToList()),
                    ExpiresIn = 86400,
                    RefreshToken = rt.Token,
                    Roles = roles,
                    TokenType = "Bearer",
                    EmailId = user.EmailId,
                    UserName = $"{user.Name}",
                    PhoneNumber = user.PhoneNumber,
                };
                return new(ResponseConstants.Success, 200, res);
            }
            else
            {
                return new ServiceResponse<LoginResponse>(ResponseConstants.InvalidInput, 400);
            }
        }


        public async Task<ServiceResponse<GetProfileModel>> GetUserProfile()
        {
            var user = await _context.Users
                                     .Where(x => x.Id == _currentUser.UserId)
                                     .Select(x => new GetProfileModel
                                     {
                                         UserId = x.Id,
                                         UserName = x.Name,
                                         EmailId = x.EmailId,
                                         PhoneNumber = x.PhoneNumber,
                                         Gender = x.Gender,
                                         DOB = x.DOB,
                                         Street = x.Street,
                                         CityId = x.CityId,
                                         CityName = x.City.CityName,
                                         StateId = x.StateId,
                                         StateName = x.State.StateName,
                                         CountryId = x.CountryId,
                                         CountryName = x.Country.Name,
                                         Pincode = x.Pincode,
                                         ProfilePicture = x.ProfilePicture != null ? Convert.ToBase64String(x.ProfilePicture) : null,
                                         IsPasswordSet = x.Password != null
                                     })
                                     .FirstOrDefaultAsync();
            return new(ResponseConstants.Success, 200, user);
        }


        public async Task<ServiceResponse<GetProfileModel>> UpdateUser(UpdateProfile model)
        {
            var user = await _context.Users.Where(x => x.Id == _currentUser.UserId).FirstOrDefaultAsync();
            user.Name = model.UserName;
            user.EmailId = model.EmailId;
            user.PhoneNumber = model.PhoneNumber;
            user.Gender = model.Gender;
            user.DOB = model.DOB;
            user.Street = model.Street;
            user.CityId = model.CityId;
            user.StateId = model.StateId;
            user.CountryId = model.CountryId;
            user.Pincode = model.Pincode;
            user.ProfilePicture = !string.IsNullOrWhiteSpace(model.ProfilePicture) ? Convert.FromBase64String(model.ProfilePicture) : null;
            _context.Update(user);
            await _context.SaveChangesAsync();
            return new(ResponseConstants.Success, 200, (await GetUserProfile()).Data);
        }
    }
}
