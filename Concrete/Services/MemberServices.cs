using Microsoft.EntityFrameworkCore;
using StansAssociates_Backend.Concrete.IServices;
using StansAssociates_Backend.Entities;
using StansAssociates_Backend.Global;
using StansAssociates_Backend.Models;

namespace StansAssociates_Backend.Concrete.Services
{
    public class MemberServices : EncryptionMethods, IMemberServices
    {
        private readonly StansassociatesAntonyContext _context;
        private readonly ICurrentUserServices _currentUser;
        private readonly IStorageServices _storageServices;

        public MemberServices(StansassociatesAntonyContext context, ICurrentUserServices currentUser, IStorageServices storageServices)
        {
            _context = context;
            _currentUser = currentUser;
            _storageServices = storageServices;
        }


        public async Task<APIResponse> AddStaff(AddStaffModel model)
        {
            bool staffExists = await _context.Users
                                             //.Where(x => x.UserRoles.Any(x => x.RoleId == 2))
                                             .AnyAsync(x => x.EmailId
                                                             .ToLower()
                                                             .Replace("", string.Empty)
                                                             .Equals(model.EmailId
                                                                          .ToLower()
                                                                          .Replace(" ", string.Empty)) ||
                                                            x.PhoneNumber.Trim() == model.PhoneNumber.Trim());

            if (staffExists)
                return new APIResponse("Email or phone number already exists.", 400);

            var staff = new User
            {
                SchoolId = model.SchoolId,
                Name = model.Name,
                EmailId = model.EmailId,
                PhoneNumber = model.PhoneNumber,
                Gender = model.Gender,
                DOB = model.DOB,
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
                        RoleId = 2
                    }
                }
            };
            await _context.AddAsync(staff);
            await _context.SaveChangesAsync();
            return new(ResponseConstants.Success, 200);
        }


        public async Task<APIResponse> UpdateStaff(UpdateStaffModel model)
        {
            var staff = await _context.Users
                                      .Where(x => x.UserRoles.Any(x => x.RoleId == 2))
                                      .Where(x => x.Id == model.Id)
                                      .FirstOrDefaultAsync();
            if (staff == null)
                return new(ResponseConstants.InvalidId, 400);
            staff.SchoolId = model.SchoolId;
            staff.Name = model.Name;
            staff.EmailId = model.EmailId;
            staff.PhoneNumber = model.PhoneNumber;
            staff.Gender = model.Gender;
            staff.DOB = model.DOB;
            staff.Street = model.Street;
            staff.CityId = model.CityId;
            staff.Pincode = model.Pincode;
            staff.StateId = model.StateId;
            staff.CountryId = model.CountryId;
            staff.ProfilePicture = !string.IsNullOrWhiteSpace(model.ProfilePicture) ? Convert.FromBase64String(model.ProfilePicture) : null;
            staff.Password = Encipher(model.Password);
            staff.UpdatedDate = DateTime.Now;
            _context.Update(staff);
            await _context.SaveChangesAsync();
            return new(ResponseConstants.Success, 200);
        }


        //public async Task<APIResponse> UpdatePassword(UpdatePasswordModel model)
        //{
        //    var staff = await _context.Users
        //                              .Where(x => x.Id == model.StaffId)
        //                              .FirstOrDefaultAsync();
        //    if (staff == null)
        //        return new(ResponseConstants.InvalidId, 400);
        //    if (Decipher(staff.Password) != model.CurrentPassword)
        //        return new("Current password is incorrect", 400);
        //    if (model.NewPassword == Decipher(staff.Password))
        //        return new("New password cannot be the same as the current password", 400);
        //    staff.Password = Encipher(model.NewPassword);
        //    staff.UpdatedDate = DateTime.Now;
        //    _context.Update(staff);
        //    await _context.SaveChangesAsync();
        //    return new(ResponseConstants.Success, 200);
        //}


        public async Task<PagedResponse<List<GetStaffModel>>> GetStaffs(PagedResponseInput model)
        {
            var staffs = await _context.Users
                                       .Where(x => _currentUser.IsAdmin || x.SchoolId == _currentUser.SchoolId)
                                       .Where(x => x.UserRoles.Any(x => x.RoleId == 2))
                                       .Where(x => string.IsNullOrWhiteSpace(model.FormattedSearchString()) ||
                                                   x.Name.ToLower().Replace(" ", "").Contains(model.FormattedSearchString()))
                                       .GroupBy(x => 1)
                                       .Select(x => new PagedResponseWithQuery<List<GetStaffModel>>
                                       {
                                           TotalRecords = x.Count(),
                                           Data = x.Select(x => new GetStaffModel
                                           {
                                               Id = x.Id,
                                               Name = x.Name,
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
                                               Password = Decipher(x.Password),
                                               SchoolId = x.SchoolId ?? 0,
                                               SchoolName = x.School.Name,
                                               IsActive = x.IsActive,
                                               ProfilePicture = x.ProfilePicture != null ? Convert.ToBase64String(x.ProfilePicture) : null,
                                               CreatedDate = x.CreatedDate,
                                               UpdatedDate = x.UpdatedDate,
                                               Permissions = _context.Modules
                                                                     .Select(a => new GetTeamPermissions
                                                                     {
                                                                         ModuleId = a.Id,
                                                                         ModuleName = a.Name,
                                                                         CanView = x.TeamPermissions.Where(b => b.ModuleId == a.Id).Select(x => x.CanView).FirstOrDefault(),
                                                                         CanAdd = x.TeamPermissions.Where(b => b.ModuleId == a.Id).Select(x => x.CanAdd).FirstOrDefault(),
                                                                         CanEdit = x.TeamPermissions.Where(b => b.ModuleId == a.Id).Select(x => x.CanEdit).FirstOrDefault(),
                                                                         CanDelete = x.TeamPermissions.Where(b => b.ModuleId == a.Id).Select(x => x.CanDelete).FirstOrDefault()
                                                                     })
                                                                     .ToList()
                                           })
                                           .Skip(model.PageSize * model.PageIndex)
                                           .Take(model.PageSize)
                                           .ToList()
                                       })
                                       .FirstOrDefaultAsync();
            return new(ResponseConstants.Success, 200, staffs?.Data, model.PageIndex, model.PageSize, staffs?.TotalRecords ?? 0);
        }


        public async Task<ServiceResponse<GetStaffModel>> GetStaff(long id)
        {
            var staff = await _context.Users
                                      .Where(x => x.UserRoles.Any(x => x.RoleId == 2))
                                      .Where(x => x.Id == id)
                                      .Select(x => new GetStaffModel
                                      {
                                          Id = x.Id,
                                          Name = x.Name,
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
                                          Password = Decipher(x.Password),
                                          SchoolId = x.SchoolId ?? 0,
                                          SchoolName = x.School.Name,
                                          IsActive = x.IsActive,
                                          ProfilePicture = x.ProfilePicture != null ? Convert.ToBase64String(x.ProfilePicture) : null,
                                          CreatedDate = x.CreatedDate,
                                          UpdatedDate = x.UpdatedDate,
                                          Permissions = _context.Modules
                                                                .Select(a => new GetTeamPermissions
                                                                {
                                                                    ModuleId = a.Id,
                                                                    ModuleName = a.Name,
                                                                    CanView = x.TeamPermissions.Where(b => b.ModuleId == a.Id).Select(x => x.CanView).FirstOrDefault(),
                                                                    CanAdd = x.TeamPermissions.Where(b => b.ModuleId == a.Id).Select(x => x.CanAdd).FirstOrDefault(),
                                                                    CanEdit = x.TeamPermissions.Where(b => b.ModuleId == a.Id).Select(x => x.CanEdit).FirstOrDefault(),
                                                                    CanDelete = x.TeamPermissions.Where(b => b.ModuleId == a.Id).Select(x => x.CanDelete).FirstOrDefault()
                                                                })
                                                                .ToList()
                                      })
                                      .FirstOrDefaultAsync();
            return new(ResponseConstants.Success, 200, staff);
        }


        public async Task<APIResponse> DeleteStaff(long id)
        {
            var staff = await _context.Users
                                      .Where(x => x.Id == id)
                                      .FirstOrDefaultAsync();
            if (staff == null)
                return new(ResponseConstants.InvalidId, 400);
            staff.IsDeleted = true;
            _context.Update(staff);
            await _context.SaveChangesAsync();
            return new(ResponseConstants.Success, 200);
        }


        public async Task<APIResponse> AddTeacher(AddTeacherModel model)
        {
            var teacherExists = await _context.Users
                                              //.Where(x => x.UserRoles.Any(x => x.RoleId == 3))
                                              .AnyAsync(x => x.EmailId
                                                              .ToLower()
                                                              .Replace("", string.Empty)
                                                              .Equals(model.EmailId
                                                                           .ToLower()
                                                                           .Replace(" ", string.Empty)) ||
                                                             x.PhoneNumber.Trim() == model.PhoneNumber.Trim());

            if (teacherExists)
                return new APIResponse("Email or phone number already exists.", 400);

            var teacher = new User
            {
                SchoolId = model.SchoolId,
                Name = model.Name,
                EmailId = model.EmailId,
                PhoneNumber = model.PhoneNumber,
                Gender = model.Gender,
                DOB = model.DOB,
                Street = model.Street,
                Pincode = model.Pincode,
                CityId = model.CityId,
                StateId = model.StateId,
                CountryId = model.CountryId,
                ProfilePicture = !string.IsNullOrWhiteSpace(model.ProfilePicture) ? Convert.FromBase64String(model.ProfilePicture) : null,
                //Password = Encipher(model.Password),
                UserRoles = new List<UserRole>
                {
                    new() {
                        RoleId = 3
                    }
                }
            };
            await _context.AddAsync(teacher);
            await _context.SaveChangesAsync();
            return new(ResponseConstants.Success, 200);
        }


        public async Task<APIResponse> UpdateTeacher(UpdateTeacherModel model)
        {
            var teacher = await _context.Users
                                        .Where(x => x.UserRoles.Any(x => x.RoleId == 3))
                                        .Where(x => x.Id == model.Id)
                                        .FirstOrDefaultAsync();
            if (teacher == null)
                return new(ResponseConstants.InvalidId, 400);
            teacher.SchoolId = model.SchoolId;
            teacher.Name = model.Name;
            teacher.EmailId = model.EmailId;
            teacher.PhoneNumber = model.PhoneNumber;
            teacher.Gender = model.Gender;
            teacher.DOB = model.DOB;
            teacher.Street = model.Street;
            teacher.CityId = model.CityId;
            teacher.Pincode = model.Pincode;
            teacher.StateId = model.StateId;
            teacher.CountryId = model.CountryId;
            teacher.ProfilePicture = !string.IsNullOrWhiteSpace(model.ProfilePicture) ? Convert.FromBase64String(model.ProfilePicture) : null;
            //teacher.Password = Encipher(model.Password);
            teacher.UpdatedDate = DateTime.Now;
            _context.Update(teacher);
            await _context.SaveChangesAsync();
            return new(ResponseConstants.Success, 200);
        }


        public async Task<PagedResponse<List<GetTeacherModel>>> GetTeachers(PagedResponseInput model)
        {
            var teachers = await _context.Users
                                         .Where(x => _currentUser.IsAdmin || x.SchoolId == _currentUser.SchoolId)
                                         .Where(x => x.UserRoles.Any(x => x.RoleId == 3))
                                         .Where(x => string.IsNullOrWhiteSpace(model.FormattedSearchString()) ||
                                                     x.Name.ToLower().Replace(" ", "").Contains(model.FormattedSearchString()))
                                         .GroupBy(x => 1)
                                         .Select(x => new PagedResponseWithQuery<List<GetTeacherModel>>
                                         {
                                             TotalRecords = x.Count(),
                                             Data = x.Select(x => new GetTeacherModel
                                             {
                                                 Id = x.Id,
                                                 Name = x.Name,
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
                                                 //Password = Decipher(x.Password),
                                                 IsActive = x.IsActive,
                                                 SchoolId = x.SchoolId ?? 0,
                                                 SchoolName = x.School.Name,
                                                 ProfilePicture = x.ProfilePicture != null ? Convert.ToBase64String(x.ProfilePicture) : null,
                                                 CreatedDate = x.CreatedDate,
                                                 UpdatedDate = x.UpdatedDate,
                                                 Permissions = _context.Modules
                                                                       .Select(a => new GetTeamPermissions
                                                                       {
                                                                           ModuleId = a.Id,
                                                                           ModuleName = a.Name,
                                                                           CanView = x.TeamPermissions.Where(b => b.ModuleId == a.Id).Select(x => x.CanView).FirstOrDefault(),
                                                                           CanAdd = x.TeamPermissions.Where(b => b.ModuleId == a.Id).Select(x => x.CanAdd).FirstOrDefault(),
                                                                           CanEdit = x.TeamPermissions.Where(b => b.ModuleId == a.Id).Select(x => x.CanEdit).FirstOrDefault(),
                                                                           CanDelete = x.TeamPermissions.Where(b => b.ModuleId == a.Id).Select(x => x.CanDelete).FirstOrDefault()
                                                                       })
                                                                       .ToList()
                                             })
                                             .Skip(model.PageSize * model.PageIndex)
                                             .Take(model.PageSize)
                                             .ToList()
                                         })
                                         .FirstOrDefaultAsync();
            return new(ResponseConstants.Success, 200, teachers?.Data, model.PageIndex, model.PageSize, teachers?.TotalRecords ?? 0);
        }


        public async Task<ServiceResponse<GetTeacherModel>> GetTeacher(long id)
        {
            var teacher = await _context.Users
                                        .Where(x => x.UserRoles.Any(x => x.RoleId == 3))
                                        .Where(x => x.Id == id)
                                        .Select(x => new GetTeacherModel
                                        {
                                            Id = x.Id,
                                            Name = x.Name,
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
                                            //Password = Decipher(x.Password),
                                            IsActive = x.IsActive,
                                            SchoolId = x.SchoolId ?? 0,
                                            SchoolName = x.School.Name,
                                            ProfilePicture = x.ProfilePicture != null ? Convert.ToBase64String(x.ProfilePicture) : null,
                                            CreatedDate = x.CreatedDate,
                                            UpdatedDate = x.UpdatedDate,
                                            Permissions = _context.Modules
                                                                  .Select(a => new GetTeamPermissions
                                                                  {
                                                                      ModuleId = a.Id,
                                                                      ModuleName = a.Name,
                                                                      CanView = x.TeamPermissions.Where(b => b.ModuleId == a.Id).Select(x => x.CanView).FirstOrDefault(),
                                                                      CanAdd = x.TeamPermissions.Where(b => b.ModuleId == a.Id).Select(x => x.CanAdd).FirstOrDefault(),
                                                                      CanEdit = x.TeamPermissions.Where(b => b.ModuleId == a.Id).Select(x => x.CanEdit).FirstOrDefault(),
                                                                      CanDelete = x.TeamPermissions.Where(b => b.ModuleId == a.Id).Select(x => x.CanDelete).FirstOrDefault()
                                                                  })
                                                                  .ToList()
                                        })
                                        .FirstOrDefaultAsync();
            return new(ResponseConstants.Success, 200, teacher);
        }


        public async Task<APIResponse> DeleteTeacher(long id)
        {
            var teacher = await _context.Users
                                        .Where(x => x.Id == id)
                                        .FirstOrDefaultAsync();
            if (teacher == null)
                return new(ResponseConstants.InvalidId, 400);
            teacher.IsDeleted = true;
            _context.Update(teacher);
            await _context.SaveChangesAsync();
            return new(ResponseConstants.Success, 200);
        }
    }
}
