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

        public DashboardServices(StansassociatesAntonyContext context)
        {
            _context = context;
        }


        public async Task<ServiceResponse<GetDashboardModel>> GetDashboard()
        {
            var dashboard = new GetDashboardModel
            {
                TotalStudents = await _context.Students.CountAsync(),
                TotalStaffs = await _context.Users.Where(x => x.UserRoles.Any(x => x.RoleId == 2)).CountAsync(),
                TotalRoutes = await _context.Routes.CountAsync(),
                TotalEarnings = await _context.StudentFeesHistories.SumAsync(x => x.Amount),
                TotalEarningsThisYear = await _context.StudentFeesHistories.Where(x => x.PaidDate.Year == DateTime.Now.Year).SumAsync(x => x.Amount),
            };
            return new(ResponseConstants.Success, 200, dashboard);
        }
    }
}
