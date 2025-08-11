using Microsoft.EntityFrameworkCore;
using StansAssociates_Backend.Concrete.IServices;
using StansAssociates_Backend.Entities;
using StansAssociates_Backend.Global;
using StansAssociates_Backend.Models;

namespace StansAssociates_Backend.Concrete.Services
{
    public class DashboardServices : IDashboardServices
    {
        private readonly StansassociatesAntonyContext _context;
        private readonly ICurrentUserServices _currentUser;

        public DashboardServices(StansassociatesAntonyContext context, ICurrentUserServices currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }


        public async Task<ServiceResponse<GetDashboardModel>> GetDashboard()
        {
            var isAdmin = _currentUser.IsAdmin;
            var schoolId = _currentUser.UserId;
            var dashboard = new GetDashboardModel
            {
                TotalStudents = await _context.Students
                                              .Where(x => isAdmin || x.SchoolId == schoolId)
                                              .CountAsync(),
                TotalStaffs = await _context.Users
                                            .Where(x => isAdmin || x.SchoolId == schoolId)
                                            .Where(x => x.UserRoles.Any(x => x.RoleId == 2))
                                            .CountAsync(),
                TotalRoutes = await _context.Routes
                                            .Where(x => isAdmin || x.SchoolId == schoolId)
                                            .CountAsync(),
                TotalEarnings = await _context.StudentFeesHistories
                                              .Where(x => isAdmin || x.Student.SchoolId == schoolId)
                                              .SumAsync(x => x.Amount),
                TotalEarningsThisYear = await _context.StudentFeesHistories
                                                      .Where(x => isAdmin || x.Student.SchoolId == schoolId)
                                                      .Where(x => x.PaidDate.Year == DateTime.Now.Year)
                                                      .SumAsync(x => x.Amount),
            };
            return new(ResponseConstants.Success, 200, dashboard);
        }
    }
}
