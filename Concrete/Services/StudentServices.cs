using Microsoft.EntityFrameworkCore;
using StansAssociates_Backend.Concrete.IServices;
using StansAssociates_Backend.Entities;
using StansAssociates_Backend.Global;
using StansAssociates_Backend.Models;

namespace StansAssociates_Backend.Concrete.Services
{
    public class StudentService : IStudentServices
    {
        private readonly StansassociatesAntonyContext _context;
        private readonly IStorageServices _storageServices;
        private readonly ICurrentUserServices _currentUser;

        public StudentService(StansassociatesAntonyContext context, IStorageServices storageServices, ICurrentUserServices currentUser)
        {
            _context = context;
            _storageServices = storageServices;
            _currentUser = currentUser;
        }


        public async Task<ServiceResponse<long>> AddStudent(AddStudentModel model)
        {
            bool studentExists = await _context.Students
                                               .AnyAsync(x => x.AdmissionNo
                                                               .ToLower()
                                                               .Replace(" ", string.Empty)
                                                               .Equals(model.AdmissionNo
                                                                            .ToLower()
                                                                            .Replace(" ", string.Empty)));
            if (studentExists)
                return new("A student with this admission number already exists.", 400);
            var student = new Student
            {
                SchoolId = _currentUser.UserId,
                Affiliation = model.Affiliation,
                AdmissionNo = model.AdmissionNo,
                RollNo = model.RollNo,
                Class = model.Class,
                Section = model.Section,
                FName = model.FName,
                LName = model.LName,
                FatherName = model.FatherName,
                Email = model.Email,
                Phone = model.Phone,
                Gender = model.Gender,
                DOB = model.DOB,
                Address = model.Address,
                RouteId = model.RouteId,
                //TotalPaid = model.TotalPaid,
                Street = model.Street,
                Pincode = model.Pincode,
                City = model.City,
                State = model.State,
                Country = model.Country,
                StudentImg = model.StudentImg != null ? (await _storageServices.UploadFile(S3Directories.StudentMedia, model.StudentImg)).Data : null,
                Year = model.Year,
                Remark = model.Remark,
                DOA = model.DOA
            };
            await _context.AddAsync(student);
            await _context.SaveChangesAsync();
            return new(ResponseConstants.Success, 200, student.Id);
        }


        public async Task<APIResponse> UpdateStudent(UpdateStudentModel model)
        {
            var student = await _context.Students
                                        .Where(x => x.Id == model.Id)
                                        .FirstOrDefaultAsync();
            if (student == null)
                return new(ResponseConstants.InvalidId, 400);
            student.Affiliation = model.Affiliation;
            student.AdmissionNo = model.AdmissionNo;
            student.RollNo = model.RollNo;
            student.Class = model.Class;
            student.Section = model.Section;
            student.FName = model.FName;
            student.LName = model.LName;
            student.FatherName = model.FatherName;
            student.Email = model.Email;
            student.Phone = model.Phone;
            student.Gender = model.Gender;
            student.DOB = model.DOB;
            student.Address = model.Address;
            student.RouteId = model.RouteId;
            student.Street = model.Street;
            student.Pincode = model.Pincode;
            student.City = model.City;
            student.State = model.State;
            student.Country = model.Country;
            student.StudentImg = model.StudentImg != null ? (await _storageServices.UploadFile(S3Directories.StudentMedia, model.StudentImg)).Data : null;
            student.Year = model.Year;
            student.Remark = model.Remark;
            student.DOA = model.DOA;
            student.UpdatedDate = DateTime.Now;
            _context.Update(student);
            await _context.SaveChangesAsync();
            return new(ResponseConstants.Success, 200);
        }


        public async Task<PagedResponse<List<GetStudentModel>>> GetStudents(GetStudentsFilterModel model)
        {
            var students = await _context.Students
                                         .Where(x => _currentUser.IsAdmin || x.SchoolId == _currentUser.UserId)
                                         .Where(x => model.SchoolId == null || x.SchoolId == model.SchoolId)
                                         .Where(x => string.IsNullOrWhiteSpace(model.Session) ||
                                                     x.Year
                                                      .ToString()
                                                      .ToLower()
                                                      .Replace(" ", string.Empty)
                                                      .Equals(model.Session
                                                                   .ToLower()
                                                                   .Replace(" ", string.Empty)))
                                         .Where(x => string.IsNullOrWhiteSpace(model.FormattedSearchString()) ||
                                                    (x.FName + x.LName).ToLower().Replace(" ", "").Contains(model.FormattedSearchString()) ||
                                                     x.AdmissionNo.ToLower().Replace(" ", "").Contains(model.FormattedSearchString()) ||
                                                     x.School.Name.ToLower().Replace(" ", "").Contains(model.FormattedSearchString()))
                                         .GroupBy(x => 1)
                                         .Select(x => new PagedResponseWithQuery<List<GetStudentModel>>
                                         {
                                             TotalRecords = x.Count(),
                                             Data = x.Select(x => new GetStudentModel
                                             {
                                                 Id = x.Id,
                                                 SchoolName = x.School.Name,
                                                 Affiliation = x.Affiliation,
                                                 AdmissionNo = x.AdmissionNo,
                                                 RollNo = x.RollNo,
                                                 Class = x.Class,
                                                 Section = x.Section,
                                                 FName = x.FName,
                                                 LName = x.LName,
                                                 FatherName = x.FatherName,
                                                 Email = x.Email,
                                                 Phone = x.Phone,
                                                 Gender = x.Gender,
                                                 DOB = x.DOB,
                                                 Address = x.Address,
                                                 RouteId = x.RouteId,
                                                 BusNo = x.Route.BusNo,
                                                 BoardingPoint = x.Route.BoardingPoint,
                                                 RouteCost = x.Route.RouteCost,
                                                 TotalPaid = x.TotalPaid,
                                                 Street = x.Street,
                                                 Pincode = x.Pincode,
                                                 City = x.City,
                                                 State = x.State,
                                                 Country = x.Country,
                                                 StudentImg = x.StudentImg,
                                                 Year = x.Year,
                                                 Remark = x.Remark,
                                                 DOA = x.DOA,
                                                 IsActive = x.IsActive,
                                                 CreatedDate = x.CreatedDate,
                                                 UpdatedDate = x.UpdatedDate
                                             })
                                             .Skip(model.PageSize * model.PageIndex)
                                             .Take(model.PageSize)
                                             .ToList()
                                         })
                                         .FirstOrDefaultAsync();
            return new(ResponseConstants.Success, 200, students?.Data, model.PageIndex, model.PageSize, students?.TotalRecords ?? 0);
        }


        public async Task<ServiceResponse<GetStudentModel>> GetStudent(long id)
        {
            var student = await _context.Students
                                        .Where(x => x.Id == id)
                                        .Select(x => new GetStudentModel
                                        {
                                            Id = x.Id,
                                            SchoolName = x.School.Name,
                                            Affiliation = x.Affiliation,
                                            AdmissionNo = x.AdmissionNo,
                                            RollNo = x.RollNo,
                                            Class = x.Class,
                                            Section = x.Section,
                                            FName = x.FName,
                                            LName = x.LName,
                                            FatherName = x.FatherName,
                                            Email = x.Email,
                                            Phone = x.Phone,
                                            Gender = x.Gender,
                                            DOB = x.DOB,
                                            Address = x.Address,
                                            RouteId = x.RouteId,
                                            BusNo = x.Route.BusNo,
                                            BoardingPoint = x.Route.BoardingPoint,
                                            RouteCost = x.Route.RouteCost,
                                            TotalPaid = x.TotalPaid,
                                            Street = x.Street,
                                            Pincode = x.Pincode,
                                            City = x.City,
                                            State = x.State,
                                            Country = x.Country,
                                            StudentImg = x.StudentImg,
                                            Year = x.Year,
                                            Remark = x.Remark,
                                            DOA = x.DOA,
                                            IsActive = x.IsActive,
                                            CreatedDate = x.CreatedDate,
                                            UpdatedDate = x.UpdatedDate
                                        })
                                        .FirstOrDefaultAsync();
            return new(ResponseConstants.Success, 200, student);
        }


        public async Task<APIResponse> DeleteStudent(long id)
        {
            var student = await _context.Students
                                        .Where(x => x.Id == id)
                                        .FirstOrDefaultAsync();
            if (student == null)
                return new(ResponseConstants.InvalidId, 400);
            student.IsDeleted = true;
            _context.Update(student);
            await _context.SaveChangesAsync();
            return new(ResponseConstants.Success, 200);
        }


        public async Task<APIResponse> AddStudentFee(AddStudentFeeModel model)
        {
            var student = await _context.Students
                                        .Where(x => x.Id == model.StudentId)
                                        .FirstOrDefaultAsync();
            if (student == null)
                return new(ResponseConstants.InvalidId, 400);
            var studentFee = new StudentFeesHistory
            {
                StudentId = model.StudentId,
                Amount = model.Amount,
                Comment = model.Comment,
                PaidMode = model.PaidMode,
                PaidDate = model.PaidDate
            };
            student.TotalPaid += model.Amount;
            await _context.AddAsync(studentFee);
            await _context.SaveChangesAsync();
            return new(ResponseConstants.Success, 200);
        }


        public async Task<PagedResponse<List<GetStudentFeeDetailsModel>>> GetStudentFees(GetStudentFeesFilterModel model)
        {
            var studentFees = await _context.Students
                                            .Where(x => model.StudentId == null || x.Id == model.StudentId)
                                            .Where(x => model.SchoolId == null || x.SchoolId == model.SchoolId)
                                            .Where(x => string.IsNullOrWhiteSpace(model.FormattedSearchString()) ||
                                                       (x.FName + x.LName).ToLower().Replace(" ", "").Contains(model.FormattedSearchString()) ||
                                                        x.AdmissionNo.ToLower().Replace(" ", "").Contains(model.FormattedSearchString()) ||
                                                        x.School.Name.ToLower().Replace(" ", "").Contains(model.FormattedSearchString()))
                                            .GroupBy(x => 1)
                                            .Select(x => new PagedResponseWithQuery<List<GetStudentFeeDetailsModel>>
                                            {
                                                TotalRecords = x.Count(),
                                                Data = x.Select(x => new GetStudentFeeDetailsModel
                                                {
                                                    StudentId = x.Id,
                                                    FName = x.FName,
                                                    LName = x.LName,
                                                    FatherName = x.FatherName,
                                                    AdmissionNo = x.AdmissionNo,
                                                    Phone = x.Phone,
                                                    Class = x.Class,
                                                    Section = x.Section,
                                                    TotalAmount = x.Route.RouteCost,
                                                    Paid = x.TotalPaid,
                                                    Due = x.Route.RouteCost - x.TotalPaid,
                                                    Fees = x.StudentFeesHistories
                                                            .Select(x => new GetStudentFeeModel
                                                            {
                                                                Id = x.Id,
                                                                Amount = x.Amount,
                                                                PaidMode = x.PaidMode,
                                                                PaidDate = x.PaidDate,
                                                                Comment = x.Comment,
                                                                CreatedDate = x.CreatedDate
                                                            })
                                                            .ToList()
                                                })
                                                .Skip(model.PageSize * model.PageIndex)
                                                .Take(model.PageSize)
                                                .ToList()
                                            })
                                            .FirstOrDefaultAsync();
            return new(ResponseConstants.Success, 200, studentFees?.Data, model.PageIndex, model.PageSize, studentFees?.TotalRecords ?? 0);
        }


        public async Task<APIResponse> AddSession(AddSessionModel model)
        {
            bool sessionExists = await _context.Sessions
                                               .AnyAsync(x => x.Name
                                                               .ToLower()
                                                               .Replace(" ", string.Empty)
                                                               .Equals(model.Name
                                                                            .ToLower()
                                                                            .Replace(" ", string.Empty)));
            if (sessionExists)
                return new APIResponse("This session name already exists.", 400);
            var session = new Session
            {
                Name = model.Name
            };
            await _context.AddAsync(session);
            await _context.SaveChangesAsync();
            return new(ResponseConstants.Success, 200);
        }


        public async Task<PagedResponse<List<GetSessionModel>>> GetSessions(PagedResponseInput model)
        {
            var sessions = await _context.Sessions
                                         .Where(x => string.IsNullOrWhiteSpace(model.FormattedSearchString()) ||
                                                     x.Name.ToLower().Replace(" ", "").Contains(model.FormattedSearchString()))
                                         .GroupBy(x => 1)
                                         .Select(x => new PagedResponseWithQuery<List<GetSessionModel>>
                                         {
                                             TotalRecords = x.Count(),
                                             Data = x.Select(x => new GetSessionModel
                                             {
                                                 Id = x.Id,
                                                 Name = x.Name,
                                                 CreatedDate = x.CreatedDate,
                                             })
                                             .Skip(model.PageSize * model.PageIndex)
                                             .Take(model.PageSize)
                                             .ToList()
                                         })
                                         .FirstOrDefaultAsync();
            return new(ResponseConstants.Success, 200, sessions?.Data, model.PageIndex, model.PageSize, sessions?.TotalRecords ?? 0);
        }
    }
}
