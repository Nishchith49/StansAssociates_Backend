using Microsoft.EntityFrameworkCore;
using StansAssociates_Backend.Concrete.IServices;
using StansAssociates_Backend.Entities;
using StansAssociates_Backend.Models;

namespace StansAssociates_Backend.Concrete.Services
{
    public class DropDownServices : IDropDownServices
    {
        private readonly StansassociatesAntonyContext _context;

        public DropDownServices(StansassociatesAntonyContext context)
        {
            _context = context;
        }


        public async Task<List<DropDownModel>> GetRouteDropDown()
        {
            var res = await _context.Routes
                                    .Select(x => new DropDownModel
                                    {
                                        Label = $"{x.BoardingPoint} | {x.BusNo}",
                                        Value = x.Id
                                    })
                                    .ToListAsync();
            return res;
        }
    }
}
