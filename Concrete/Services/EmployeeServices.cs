using Microsoft.EntityFrameworkCore;
using StansAssociates_Backend.Concrete.IServices;
using StansAssociates_Backend.Entities;
using StansAssociates_Backend.Global;
using StansAssociates_Backend.Models;

namespace StansAssociates_Backend.Concrete.Services
{
    public class EmployeeServices : IEmployeeServices
    {
        private readonly StansassociatesAntonyContext _context;

        public EmployeeServices(StansassociatesAntonyContext context)
        {
            _context = context;
        }


        public async Task<APIResponse> AddEmployee(AddEmployeeModel model)
        {
            var employeeExists = await _context.Employees
                                               .AnyAsync(x => x.EmpCode == model.EmpCode);
            if (employeeExists)
                return new APIResponse("This employee code already exists.", 400);
            var employee = new Employee
            {
                EmpCode = model.EmpCode,
                Name = model.Name,
                Designation = model.Designation,
                TDP = model.Tdp,
                TAD = model.Tad,
                THL = model.Thl
            };
            await _context.AddAsync(employee);
            await _context.SaveChangesAsync();
            return new(ResponseConstants.Success, 200);
        }


        public async Task<APIResponse> UpdateEmployee(UpdateEmployeeModel model)
        {
            var employee = await _context.Employees
                                         .Where(x => x.Id == model.Id)
                                         .FirstOrDefaultAsync();
            if (employee == null)
                return new(ResponseConstants.InvalidId, 400);
            var employeeExists = await _context.Employees
                                               .AnyAsync(x => x.EmpCode == model.EmpCode && x.Id != model.Id);
            if (employeeExists)
                return new APIResponse("This employee code already exists.", 400);
            employee.EmpCode = model.EmpCode;
            employee.Name = model.Name;
            employee.Designation = model.Designation;
            employee.TDP = model.Tdp;
            employee.TAD = model.Tad;
            employee.THL = model.Thl;
            employee.UpdatedDate = DateTime.Now;
            _context.Update(employee);
            await _context.SaveChangesAsync();
            return new(ResponseConstants.Success, 200);
        }


        public async Task<APIResponse> EnableOrDisableEmployee(long id)
        {
            var employee = await _context.Employees
                                         .Where(x => x.Id == id)
                                         .FirstOrDefaultAsync();
            if (employee == null)
                return new(ResponseConstants.InvalidId, 400);
            employee.IsActive = !employee.IsActive;
            employee.UpdatedDate = DateTime.Now;
            _context.Update(employee);
            await _context.SaveChangesAsync();
            return new(ResponseConstants.Success, 200);
        }


        public async Task<PagedResponse<List<GetEmployeeModel>>> GetEmployees(GetEmployeesFilterModel model)
        {
            var routes = await _context.Employees
                                       .Where(x => string.IsNullOrWhiteSpace(model.FormattedSearchString()) ||
                                                   x.EmpCode.ToLower().Replace(" ", "").Contains(model.FormattedSearchString()) ||
                                                   x.Name.ToLower().Replace(" ", "").Contains(model.FormattedSearchString()) ||
                                                   x.Designation.ToLower().Replace(" ", "").Contains(model.FormattedSearchString()))
                                       .GroupBy(x => 1)
                                       .Select(x => new PagedResponseWithQuery<List<GetEmployeeModel>>
                                       {
                                           TotalRecords = x.Count(),
                                           Data = x.Select(x => new GetEmployeeModel
                                           {
                                               Id = x.Id,
                                               EmpCode = x.EmpCode,
                                               Name = x.Name,
                                               Designation = x.Designation,
                                               Tdp = x.TDP,
                                               Tad = x.TAD,
                                               Thl = x.THL,
                                               IsActive = x.IsActive,
                                               CreatedDate = x.CreatedDate,
                                               UpdatedDate = x.UpdatedDate
                                           })
                                           .Skip(model.PageSize * model.PageIndex)
                                           .Take(model.PageSize)
                                           .ToList()
                                       })
                                       .FirstOrDefaultAsync();
            return new(ResponseConstants.Success, 200, routes?.Data, model.PageIndex, model.PageSize, routes?.TotalRecords ?? 0);
        }


        public async Task<ServiceResponse<GetEmployeeModel>> GetEmployee(long id)
        {
            var employee = await _context.Employees
                                         .Where(x => x.Id == id)
                                         .Select(x => new GetEmployeeModel
                                         {
                                             Id = x.Id,
                                             EmpCode = x.EmpCode,
                                             Name = x.Name,
                                             Designation = x.Designation,
                                             Tdp = x.TDP,
                                             Tad = x.TAD,
                                             Thl = x.THL,
                                             IsActive = x.IsActive,
                                             CreatedDate = x.CreatedDate,
                                             UpdatedDate = x.UpdatedDate
                                         })
                                         .FirstOrDefaultAsync();
            return new(ResponseConstants.Success, 200, employee);
        }


        public async Task<APIResponse> DeleteEmployee(long id)
        {
            var employee = _context.Employees
                                   .Where(x => x.Id == id)
                                   .FirstOrDefault();
            if (employee == null)
                return new(ResponseConstants.InvalidId, 400);
            employee.IsDeleted = true;
            _context.Update(employee);
            await _context.SaveChangesAsync();
            return new(ResponseConstants.Success, 200);
        }


        public async Task<APIResponse> AddEmployeeAttendance(AddEmployeeAttendanceModel model)
        {
            var attendanceExists = await _context.EmployeeAttendances
                                                 .AnyAsync(x => x.EmployeeId == model.EmployeeId &&
                                                                x.AttendanceDate == model.AttendanceDate);
            if (attendanceExists)
                return new APIResponse("Attendance for this employee on this date already exists.", 400);
            var attendance = new EmployeeAttendance
            {
                EmployeeId = model.EmployeeId,
                AttendanceDate = model.AttendanceDate,
                Status = model.Status,
                Remarks = model.Remarks
            };
            await _context.AddAsync(attendance);
            await _context.SaveChangesAsync();
            return new(ResponseConstants.Success, 200);
        }


        public async Task<APIResponse> BulkAddEmployeeAttendance(BulkAddEmployeeAttendanceModel model)
        {
            var employees = await _context.Employees
                                          .Where(x => x.IsActive)
                                          .Select(x => x.Id)
                                          .ToListAsync();
            if (!employees.Any())
                return new APIResponse("No active employees found.", 400);

            var existing = await _context.EmployeeAttendances
                                         .Where(x => x.AttendanceDate == model.AttendanceDate && employees.Contains(x.EmployeeId))
                                         .Select(x => x.EmployeeId)
                                         .ToListAsync();
            var existingSet = new HashSet<long>(existing);

            var finalRemarks = model.Remarks ?? model.Status switch
            {
                (int)AttendanceStatus.Present => "Present",
                (int)AttendanceStatus.Absent => "Absent",
                (int)AttendanceStatus.Leave => "Leave",
                (int)AttendanceStatus.HalfDay => "Half Day",
                (int)AttendanceStatus.WeeklyOff => "Weekly Off",
                (int)AttendanceStatus.Holiday => "Holiday",
                (int)AttendanceStatus.WorkFromHome => "Work From Home",
                (int)AttendanceStatus.OnDuty => "On Duty",
                _ => "Attendance"
            };

            var toInsert = new List<EmployeeAttendance>(capacity: Math.Max(0, employees.Count - existingSet.Count));
            foreach (var empId in employees)
            {
                if (existingSet.Contains(empId)) continue;

                toInsert.Add(new EmployeeAttendance
                {
                    EmployeeId = empId,
                    AttendanceDate = model.AttendanceDate,
                    Status = model.Status,
                    Remarks = finalRemarks
                });
            }
            if (toInsert.Any())
            {
                await _context.EmployeeAttendances.AddRangeAsync(toInsert);
                await _context.SaveChangesAsync();
            }
            return new(ResponseConstants.Success, 200);
        }


        public async Task<APIResponse> UpdateEmployeeAttendance(UpdateEmployeeAttendanceModel model)
        {
            var attendance = await _context.EmployeeAttendances
                                           .Where(x => x.Id == model.Id)
                                           .FirstOrDefaultAsync();
            if (attendance == null)
                return new(ResponseConstants.InvalidId, 400);
            var attendanceExists = await _context.EmployeeAttendances
                                                 .AnyAsync(x => x.EmployeeId == model.EmployeeId &&
                                                                x.AttendanceDate == model.AttendanceDate &&
                                                                x.Id != model.Id);
            if (attendanceExists)
                return new APIResponse("Attendance for this employee on this date already exists.", 400);
            attendance.EmployeeId = model.EmployeeId;
            attendance.AttendanceDate = model.AttendanceDate;
            attendance.Status = model.Status;
            attendance.Remarks = model.Remarks;
            attendance.UpdatedDate = DateTime.Now;
            _context.Update(attendance);
            await _context.SaveChangesAsync();
            return new(ResponseConstants.Success, 200);
        }


        public async Task<PagedResponse<List<GetEmployeeAttendanceModel>>> GetEmployeeAttendances(GetEmployeeAttendancesFilterModel model)
        {
            var attendances = await _context.EmployeeAttendances
                                            .Where(x => model.EmployeeId == null || x.EmployeeId == model.EmployeeId)
                                            .Where(x => model.FromDate == null || x.AttendanceDate >= model.FromDate)
                                            .Where(x => model.ToDate == null || x.AttendanceDate <= model.ToDate)
                                            .Where(x => string.IsNullOrWhiteSpace(model.FormattedSearchString()) ||
                                                        x.Employee.EmpCode.ToLower().Replace(" ", "").Contains(model.FormattedSearchString()) ||
                                                        x.Employee.Name.ToLower().Replace(" ", "").Contains(model.FormattedSearchString()) ||
                                                        x.Employee.Designation.ToLower().Replace(" ", "").Contains(model.FormattedSearchString()))
                                            .GroupBy(x => 1)
                                            .Select(x => new PagedResponseWithQuery<List<GetEmployeeAttendanceModel>>
                                            {
                                                TotalRecords = x.Count(),
                                                Data = x.Select(x => new GetEmployeeAttendanceModel
                                                {
                                                    Id = x.Id,
                                                    EmployeeId = x.EmployeeId,
                                                    EmployeeName = x.Employee.Name,
                                                    AttendanceDate = x.AttendanceDate,
                                                    Status = x.Status,
                                                    Remarks = x.Remarks,
                                                    CreatedDate = x.CreatedDate,
                                                    UpdatedDate = x.UpdatedDate
                                                })
                                                .Skip(model.PageSize * model.PageIndex)
                                                .Take(model.PageSize)
                                                .ToList()
                                            })
                                            .FirstOrDefaultAsync();
            return new(ResponseConstants.Success, 200, attendances?.Data, model.PageIndex, model.PageSize, attendances?.TotalRecords ?? 0);
        }
    }
}