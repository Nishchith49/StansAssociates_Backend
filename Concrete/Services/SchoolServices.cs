using Microsoft.EntityFrameworkCore;
using StansAssociates_Backend.Concrete.IServices;
using StansAssociates_Backend.Entities;
using StansAssociates_Backend.Global;
using StansAssociates_Backend.Models;

namespace StansAssociates_Backend.Concrete.Services
{
    public class SchoolServices : EncryptionMethods, ISchoolServices
    {
        private readonly StansassociatesAntonyContext _context;
        private readonly ICurrentUserServices _currentUser;
        private readonly IStorageServices _storageServices;

        public SchoolServices(StansassociatesAntonyContext context, ICurrentUserServices currentUser, IStorageServices storageServices)
        {
            _context = context;
            _currentUser = currentUser;
            _storageServices = storageServices;
        }


        public async Task<APIResponse> AddSchool(AddSchoolModel model)
        {
            var schoolExists = await _context.Users
                                             .Where(x => x.UserRoles.Any(x => x.RoleId == 4))
                                             .Where(x => x.EmailId
                                                          .ToLower()
                                                          .Replace(" ", string.Empty)
                                                          .Equals(model.EmailId
                                                                       .ToLower()
                                                                       .Replace(" ", string.Empty)) ||
                                                         x.Name
                                                          .ToLower()
                                                          .Replace(" ", string.Empty)
                                                          .Equals(model.Name
                                                                       .ToLower()
                                                                       .Replace(" ", string.Empty)))
                                             .FirstOrDefaultAsync();
            if (schoolExists != null)
            {
                if (schoolExists.EmailId.ToLower().Replace(" ", "") == model.EmailId.ToLower().Replace(" ", ""))
                    return new APIResponse("A school with this email already exists.", 400);

                if (schoolExists.Name.ToLower().Replace(" ", "") == model.Name.ToLower().Replace(" ", ""))
                    return new APIResponse("A school with this name already exists.", 400);
            }

            var school = new User
            {
                Name = model.Name,
                EmailId = model.EmailId,
                PhoneNumber = model.PhoneNumber,
                Street = model.Street,
                Pincode = model.Pincode,
                CityId = model.CityId,
                StateId = model.StateId,
                CountryId = model.CountryId,
                ProfilePicture = !string.IsNullOrWhiteSpace(model.ProfilePicture) ? Convert.FromBase64String(model.ProfilePicture) : null,
                Password = Encipher(model.Password),
                UserRoles = new List<UserRole>
                {
                    new() {
                        RoleId = 4
                    }
                }
            };
            await _context.AddAsync(school);
            await _context.SaveChangesAsync();
            return new(ResponseConstants.Success, 200);
        }


        public async Task<APIResponse> UpdateSchool(UpdateSchoolModel model)
        {
            var school = await _context.Users
                                       .Where(x => x.UserRoles.Any(x => x.RoleId == 4))
                                       .Where(x => x.Id == model.Id)
                                       .FirstOrDefaultAsync();
            if (school == null)
                return new(ResponseConstants.InvalidId, 400);
            school.Name = model.Name;
            school.EmailId = model.EmailId;
            school.PhoneNumber = model.PhoneNumber;
            school.Street = model.Street;
            school.CityId = model.CityId;
            school.Pincode = model.Pincode;
            school.StateId = model.StateId;
            school.CountryId = model.CountryId;
            school.ProfilePicture = !string.IsNullOrWhiteSpace(model.ProfilePicture) ? Convert.FromBase64String(model.ProfilePicture) : null;
            school.Password = Encipher(model.Password);
            school.UpdatedDate = DateTime.Now;
            _context.Update(school);
            await _context.SaveChangesAsync();
            return new(ResponseConstants.Success, 200);
        }


        public async Task<PagedResponse<List<GetSchoolModel>>> GetSchools(PagedResponseInput model)
        {
            var schools = await _context.Users
                                        .Where(x => x.UserRoles.Any(x => x.RoleId == 4))
                                        .Where(x => string.IsNullOrWhiteSpace(model.FormattedSearchString()) ||
                                                    x.Name.ToLower().Replace(" ", "").Contains(model.FormattedSearchString()))
                                        .GroupBy(x => 1)
                                        .Select(x => new PagedResponseWithQuery<List<GetSchoolModel>>
                                        {
                                            TotalRecords = x.Count(),
                                            Data = x.Select(x => new GetSchoolModel
                                            {
                                                Id = x.Id,
                                                Name = x.Name,
                                                EmailId = x.EmailId,
                                                PhoneNumber = x.PhoneNumber,
                                                Street = x.Street,
                                                CityId = x.CityId,
                                                CityName = x.City.CityName,
                                                StateId = x.StateId,
                                                StateName = x.State.StateName,
                                                CountryId = x.CountryId,
                                                CountryName = x.Country.Name,
                                                Pincode = x.Pincode,
                                                Password = Decipher(x.Password),
                                                IsActive = x.IsActive,
                                                ProfilePicture = x.ProfilePicture != null ? Convert.ToBase64String(x.ProfilePicture) : null,
                                                CreatedDate = x.CreatedDate,
                                                UpdatedDate = x.UpdatedDate
                                            })
                                            .Skip(model.PageSize * model.PageIndex)
                                            .Take(model.PageSize)
                                            .ToList()
                                        })
                                        .FirstOrDefaultAsync();
            return new(ResponseConstants.Success, 200, schools?.Data, model.PageIndex, model.PageSize, schools?.TotalRecords ?? 0);
        }


        public async Task<ServiceResponse<GetSchoolModel>> GetSchool(long id)
        {
            var school = await _context.Users
                                       .Where(x => x.UserRoles.Any(x => x.RoleId == 4))
                                       .Where(x => x.Id == id)
                                       .Select(x => new GetSchoolModel
                                       {
                                           Id = x.Id,
                                           Name = x.Name,
                                           EmailId = x.EmailId,
                                           PhoneNumber = x.PhoneNumber,
                                           Street = x.Street,
                                           CityId = x.CityId,
                                           CityName = x.City.CityName,
                                           StateId = x.StateId,
                                           StateName = x.State.StateName,
                                           CountryId = x.CountryId,
                                           CountryName = x.Country.Name,
                                           Pincode = x.Pincode,
                                           Password = Decipher(x.Password),
                                           IsActive = x.IsActive,
                                           ProfilePicture = x.ProfilePicture != null ? Convert.ToBase64String(x.ProfilePicture) : null,
                                           CreatedDate = x.CreatedDate,
                                           UpdatedDate = x.UpdatedDate
                                       })
                                       .FirstOrDefaultAsync();
            return new(ResponseConstants.Success, 200, school);
        }


        public async Task<APIResponse> DeleteSchool(long id)
        {
            var school = await _context.Users
                                       .Where(x => x.Id == id)
                                       .FirstOrDefaultAsync();
            if (school == null)
                return new(ResponseConstants.InvalidId, 400);
            school.IsDeleted = true;
            _context.Update(school);
            await _context.SaveChangesAsync();
            return new(ResponseConstants.Success, 200);
        }
    }
}