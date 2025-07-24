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
                                             .Where(x => x.UserRoles.Any(x => x.RoleId == 2))
                                             .AnyAsync(x => x.EmailId == model.EmailId || x.PhoneNumber == model.PhoneNumber);

            if (staffExists)
                return new APIResponse("A staff with this email or phone number already exists.", 400);

            var staff = new User
            {
                Name = model.Name,
                EmailId = model.EmailId,
                PhoneNumber = model.PhoneNumber,
                Gender = model.Gender,
                DOB = model.DOB,
                Street = model.Street,
                Pincode = model.Pincode,
                City = model.City,
                State = model.State,
                Country = model.Country,
                ProfilePicture = !string.IsNullOrWhiteSpace(model.ProfilePicture) ? (await _storageServices.UploadFile(S3Directories.ProfileMedia, model.ProfilePicture)).Data : null,
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
            staff.Name = model.Name;
            staff.EmailId = model.EmailId;
            staff.PhoneNumber = model.PhoneNumber;
            staff.Gender = model.Gender;
            staff.DOB = model.DOB;
            staff.Street = model.Street;
            staff.City = model.City;
            staff.Pincode = model.Pincode;
            staff.State = model.State;
            staff.Country = model.Country;
            staff.ProfilePicture = !string.IsNullOrWhiteSpace(model.ProfilePicture) ? (await _storageServices.UploadFile(S3Directories.ProfileMedia, model.ProfilePicture)).Data : null;
            staff.Password = Encipher(model.Password);
            staff.UpdatedDate = DateTime.Now;
            _context.Update(staff);
            await _context.SaveChangesAsync();
            return new(ResponseConstants.Success, 200);
        }


        public async Task<APIResponse> UpdatePassword(UpdatePasswordModel model)
        {
            var staff = await _context.Users
                                      .Where(x => x.Id == model.StaffId)
                                      .FirstOrDefaultAsync();
            if (staff == null)
                return new(ResponseConstants.InvalidId, 400);
            if (Decipher(staff.Password) != model.CurrentPassword)
                return new("Current password is incorrect", 400);
            if (model.NewPassword == Decipher(staff.Password))
                return new("New password cannot be the same as the current password", 400);
            staff.Password = Encipher(model.NewPassword);
            staff.UpdatedDate = DateTime.Now;
            _context.Update(staff);
            await _context.SaveChangesAsync();
            return new(ResponseConstants.Success, 200);
        }


        public async Task<PagedResponse<List<GetStaffModel>>> GetStaffs(PagedResponseInput model)
        {
            var staffs = await _context.Users
                                       .Where(x => x.UserRoles.Any(x => x.RoleId == 2))
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
                                               City = x.City,
                                               State = x.State,
                                               Country = x.Country,
                                               Pincode = x.Pincode,
                                               Password = Decipher(x.Password),
                                               IsActive = x.IsActive,
                                               ProfilePicture = x.ProfilePicture,
                                               CreatedDate = x.CreatedDate,
                                               UpdatedDate = x.UpdatedDate
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
                                          City = x.City,
                                          State = x.State,
                                          Country = x.Country,
                                          Pincode = x.Pincode,
                                          Password = Decipher(x.Password),
                                          IsActive = x.IsActive,
                                          ProfilePicture = x.ProfilePicture,
                                          CreatedDate = x.CreatedDate,
                                          UpdatedDate = x.UpdatedDate
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
                                              .Where(x => x.UserRoles.Any(x => x.RoleId == 3))
                                              .AnyAsync(x => x.EmailId == model.EmailId || x.PhoneNumber == model.PhoneNumber);

            if (teacherExists)
                return new APIResponse("A teacher with this email or phone number already exists.", 400);

            var teacher = new User
            {
                Name = model.Name,
                EmailId = model.EmailId,
                PhoneNumber = model.PhoneNumber,
                Gender = model.Gender,
                DOB = model.DOB,
                Street = model.Street,
                Pincode = model.Pincode,
                City = model.City,
                State = model.State,
                Country = model.Country,
                ProfilePicture = !string.IsNullOrWhiteSpace(model.ProfilePicture) ? (await _storageServices.UploadFile(S3Directories.ProfileMedia, model.ProfilePicture)).Data : null,
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
            teacher.Name = model.Name;
            teacher.EmailId = model.EmailId;
            teacher.PhoneNumber = model.PhoneNumber;
            teacher.Gender = model.Gender;
            teacher.DOB = model.DOB;
            teacher.Street = model.Street;
            teacher.City = model.City;
            teacher.Pincode = model.Pincode;
            teacher.State = model.State;
            teacher.Country = model.Country;
            teacher.ProfilePicture = !string.IsNullOrWhiteSpace(model.ProfilePicture) ? (await _storageServices.UploadFile(S3Directories.ProfileMedia, model.ProfilePicture)).Data : null;
            //teacher.Password = Encipher(model.Password);
            teacher.UpdatedDate = DateTime.Now;
            _context.Update(teacher);
            await _context.SaveChangesAsync();
            return new(ResponseConstants.Success, 200);
        }


        public async Task<PagedResponse<List<GetTeacherModel>>> GetTeachers(PagedResponseInput model)
        {
            var teachers = await _context.Users
                                         .Where(x => x.UserRoles.Any(x => x.RoleId == 3))
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
                                                 City = x.City,
                                                 State = x.State,
                                                 Country = x.Country,
                                                 Pincode = x.Pincode,
                                                 //Password = Decipher(x.Password),
                                                 IsActive = x.IsActive,
                                                 ProfilePicture = x.ProfilePicture,
                                                 CreatedDate = x.CreatedDate,
                                                 UpdatedDate = x.UpdatedDate
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
                                            City = x.City,
                                            State = x.State,
                                            Country = x.Country,
                                            Pincode = x.Pincode,
                                            //Password = Decipher(x.Password),
                                            IsActive = x.IsActive,
                                            ProfilePicture = x.ProfilePicture,
                                            CreatedDate = x.CreatedDate,
                                            UpdatedDate = x.UpdatedDate
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
