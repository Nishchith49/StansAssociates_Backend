using Microsoft.EntityFrameworkCore;
using StansAssociates_Backend.Concrete.IServices;
using StansAssociates_Backend.Entities;
using StansAssociates_Backend.Global;
using StansAssociates_Backend.Models;

namespace StansAssociates_Backend.Concrete.Services
{
    public class DropDownServices : IDropDownServices
    {
        private readonly StansassociatesAntonyContext _context;
        private readonly ICurrentUserServices _currentUser;

        public DropDownServices(StansassociatesAntonyContext context, ICurrentUserServices currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }


        public async Task<List<RouteDropDownModel>> GetRouteDropDown()
        {
            var res = await _context.Routes
                                    .Where(x => _currentUser.IsAdmin || x.SchoolId == _currentUser.UserId)
                                    .Select(x => new RouteDropDownModel
                                    {
                                        SchoolName = x.School.Name,
                                        BusNo = x.BusNo,
                                        BoardingPoint = x.BoardingPoint,
                                        Id = x.Id
                                    })
                                    .ToListAsync();
            return res;
        }


        public async Task<List<DropDownModel>> GetModuleDropDown()
        {
            var res = await _context.Modules
                                    .Select(x => new DropDownModel
                                    {
                                        Label = x.Name,
                                        Value = x.Id
                                    })
                                    .ToListAsync();
            return res;
        }


        public async Task<List<DropDownModel>> GetSchoolDropDown()
        {
            var res = await _context.Users
                                    .Where(x => x.UserRoles.Any(x => x.RoleId == 4))
                                    .Select(x => new DropDownModel
                                    {
                                        Label = x.Name,
                                        Value = x.Id
                                    })
                                    .ToListAsync();
            return res;
        }
    }
}
