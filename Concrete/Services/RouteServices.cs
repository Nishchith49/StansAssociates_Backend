using Microsoft.EntityFrameworkCore;
using StansAssociates_Backend.Concrete.IServices;
using StansAssociates_Backend.Entities;
using StansAssociates_Backend.Global;
using StansAssociates_Backend.Models;
using Route = StansAssociates_Backend.Entities.Route;

namespace StansAssociates_Backend.Concrete.Services
{
    public class RouteServices : IRouteServices
    {
        private readonly StansassociatesAntonyContext _context;

        public RouteServices(StansassociatesAntonyContext context)
        {
            _context = context;
        }


        public async Task<APIResponse> AddRoute(AddRouteModel model)
        {
            var routeExists = await _context.Routes
                                            .AnyAsync(x => x.SchoolId == model.SchoolId &&
                                                           x.BusNo
                                                            .ToLower()
                                                            .Replace(" ", string.Empty)
                                                            .Equals(model.BusNo
                                                                         .ToLower()
                                                                         .Replace(" ", string.Empty)) &&
                                                           x.BoardingPoint
                                                            .ToLower()
                                                            .Replace(" ", string.Empty)
                                                            .Equals(model.BoardingPoint
                                                                         .ToLower()
                                                                         .Replace(" ", string.Empty)));
            if (routeExists)
                return new APIResponse("This route already exists.", 400);
            var route = new Route
            {
                SchoolId = model.SchoolId,
                BusNo = model.BusNo,
                BoardingPoint = model.BoardingPoint,
                RouteCost = model.RouteCost,
                CreatedDate = DateTime.Now
            };
            await _context.AddAsync(route);
            await _context.SaveChangesAsync();
            return new(ResponseConstants.Success, 200);
        }

         
        public async Task<APIResponse> UpdateRoute(UpdateRouteModel model)
        {
            var route = await _context.Routes
                                      .Where(x => x.Id == model.Id)
                                      .FirstOrDefaultAsync();
            if (route == null)
                return new(ResponseConstants.InvalidId, 400);
            route.SchoolId = model.SchoolId;
            route.BusNo = model.BusNo;
            route.BoardingPoint = model.BoardingPoint;
            route.RouteCost = model.RouteCost;
            route.UpdatedDate = DateTime.Now;
            _context.Update(route);
            await _context.SaveChangesAsync();
            return new(ResponseConstants.Success, 200);
        }


        public async Task<PagedResponse<List<GetRouteModel>>> GetRoutes(GetRoutesFilterModel model)
        {
            var routes = await _context.Routes
                                       .Where(x => model.SchoolId == null || x.SchoolId == model.SchoolId)
                                       .Where(x => string.IsNullOrWhiteSpace(model.FormattedSearchString()) ||
                                                   x.BoardingPoint.ToLower().Replace(" ", "").Contains(model.FormattedSearchString()) ||
                                                   x.BusNo.ToLower().Replace(" ", "").Contains(model.FormattedSearchString()) ||
                                                   x.School.Name.ToLower().Replace(" ", "").Contains(model.FormattedSearchString()))
                                       .GroupBy(x => 1)
                                       .Select(x => new PagedResponseWithQuery<List<GetRouteModel>>
                                       {
                                           TotalRecords = x.Count(),
                                           Data = x.Select(x => new GetRouteModel
                                           {
                                               Id = x.Id,
                                               SchoolId = x.SchoolId,
                                               SchoolName = x.School.Name,
                                               BusNo = x.BusNo,
                                               BoardingPoint = x.BoardingPoint,
                                               RouteCost = x.RouteCost,
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


        public async Task<ServiceResponse<GetRouteModel>> GetRoute(long id)
        {
            var route = await _context.Routes
                                      .Where(x => x.Id == id)
                                      .Select(x => new GetRouteModel
                                      {
                                          Id = x.Id,
                                          SchoolId = x.SchoolId,
                                          SchoolName = x.School.Name,
                                          BusNo = x.BusNo,
                                          BoardingPoint = x.BoardingPoint,
                                          RouteCost = x.RouteCost,
                                          IsActive = x.IsActive,
                                          CreatedDate = x.CreatedDate,
                                          UpdatedDate = x.UpdatedDate
                                      })
                                      .FirstOrDefaultAsync();
            return new(ResponseConstants.Success, 200, route);
        }


        public async Task<APIResponse> DeleteRoute(long id)
        {
            var route = await _context.Routes
                                      .Where(x => x.Id == id)
                                      .FirstOrDefaultAsync();
            if (route == null)
                return new(ResponseConstants.InvalidId, 400);
            route.IsDeleted = true;
            _context.Update(route);
            await _context.SaveChangesAsync();
            return new(ResponseConstants.Success, 200);
        }
    }
}
