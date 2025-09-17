using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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


        public async Task<List<RouteDropDownModel>> GetRouteDropDown(long? schoolId)
        {
            var res = await _context.Routes
                                    .Where(x => _currentUser.IsAdmin || x.SchoolId == _currentUser.SchoolId)
                                    .Where(x => schoolId == null || x.SchoolId == schoolId)
                                    .Select(x => new RouteDropDownModel
                                    {
                                        SchoolName = x.School.Name,
                                        BusNo = x.BusNo,
                                        BoardingPoint = x.BoardingPoint,
                                        RouteCost = x.RouteCost,
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


        public async Task<List<DropDownModel>> GetCityDropDown(long stateId)
        {
            var res = await _context.Cities
                                    .Where(x => x.StateId == stateId)
                                    .Select(x => new DropDownModel
                                    {
                                        Label = x.CityName,
                                        Value = x.Id
                                    })
                                    .ToListAsync();
            return res;
        }


        public async Task<List<DropDownModel>> GetStateDropDown(long countryId)
        {
            var res = await _context.States
                                    .Where(x => x.CountryId == countryId)
                                    .Select(x => new DropDownModel
                                    {
                                        Label = x.StateName,
                                        Value = x.Id
                                    })
                                    .ToListAsync();
            return res;
        }


        public async Task<List<DropDownModel>> GetCountryDropDown()
        {
            var res = await _context.Countries
                                    .Select(x => new DropDownModel
                                    {
                                        Label = x.Name,
                                        Value = x.Id
                                    })
                                    .ToListAsync();
            return res;
        }


        public async Task<List<DropDownModel>> GetEmployeeDropDown()
        {
            var res = await _context.Employees
                                    .Select(x => new DropDownModel
                                    {
                                        Label = x.Name,
                                        Value = x.Id
                                    })
                                    .ToListAsync();
            return res;
        }


        public async Task<APIResponse> InsertIndiaData()
        {
            using var client = new HttpClient();

            var countryRequest = new HttpRequestMessage(HttpMethod.Get, "https://api.countrystatecity.in/v1/countries/IN");
            countryRequest.Headers.Add("X-CSCAPI-KEY", "bkdQNnVSYWRabjhoeUxrUEdvNEIyUWhGYzhYT0hoUklLc2VwRFJqSg==");
            var countryResponse = await client.SendAsync(countryRequest);
            countryResponse.EnsureSuccessStatusCode();
            var countryDetails = JsonConvert.DeserializeObject<countryDetailsJson>(await countryResponse.Content.ReadAsStringAsync());

            var india = new Country
            {
                Name = countryDetails.name,
                Iso2 = countryDetails.iso2,
                Latitude = Convert.ToDecimal(countryDetails.latitude ?? "0"),
                Longitude = Convert.ToDecimal(countryDetails.longitude ?? "0"),
                States = new List<State>()
            };

            var stateRequest = new HttpRequestMessage(HttpMethod.Get, "https://api.countrystatecity.in/v1/countries/IN/states");
            stateRequest.Headers.Add("X-CSCAPI-KEY", "bkdQNnVSYWRabjhoeUxrUEdvNEIyUWhGYzhYT0hoUklLc2VwRFJqSg==");
            var stateResponse = await client.SendAsync(stateRequest);
            stateResponse.EnsureSuccessStatusCode();
            var states = JsonConvert.DeserializeObject<List<statesJson>>(await stateResponse.Content.ReadAsStringAsync());

            foreach (var state in states)
            {
                var stateDetailsRequest = new HttpRequestMessage(HttpMethod.Get, $"https://api.countrystatecity.in/v1/countries/IN/states/{state.iso2}");
                stateDetailsRequest.Headers.Add("X-CSCAPI-KEY", "bkdQNnVSYWRabjhoeUxrUEdvNEIyUWhGYzhYT0hoUklLc2VwRFJqSg==");
                var stateDetailsResponse = await client.SendAsync(stateDetailsRequest);

                if (!stateDetailsResponse.IsSuccessStatusCode) continue;

                var stateDetail = JsonConvert.DeserializeObject<stateDetailsJson>(await stateDetailsResponse.Content.ReadAsStringAsync());

                var s = new State
                {
                    StateName = stateDetail.name,
                    Iso2 = stateDetail.iso2,
                    Latitude = Convert.ToDecimal(stateDetail.latitude ?? "0"),
                    Longitude = Convert.ToDecimal(stateDetail.longitude ?? "0"),
                    Cities = new List<City>()
                };

                var cityRequest = new HttpRequestMessage(HttpMethod.Get, $"https://api.countrystatecity.in/v1/countries/IN/states/{state.iso2}/cities");
                cityRequest.Headers.Add("X-CSCAPI-KEY", "bkdQNnVSYWRabjhoeUxrUEdvNEIyUWhGYzhYT0hoUklLc2VwRFJqSg==");
                var cityResponse = await client.SendAsync(cityRequest);

                if (cityResponse.IsSuccessStatusCode)
                {
                    var cities = JsonConvert.DeserializeObject<List<citiesJson>>(await cityResponse.Content.ReadAsStringAsync());

                    foreach (var city in cities)
                    {
                        s.Cities.Add(new City
                        {
                            CityName = city.name,
                            Latitude = Convert.ToDecimal(city.latitude ?? "0"),
                            Longitude = Convert.ToDecimal(city.longitude ?? "0")
                        });
                    }
                }

                india.States.Add(s);
            }

            await _context.Countries.AddAsync(india);
            await _context.SaveChangesAsync();

            return new APIResponse(ResponseConstants.Success, 200);
        }


        public class countriesJson
        {
            public int id { get; set; }
            public string name { get; set; }
            public string iso3 { get; set; }
            public string iso2 { get; set; }
            public string phonecode { get; set; }
            public string capital { get; set; }
            public string currency { get; set; }
            public string native { get; set; }
            public string emoji { get; set; }
        }


        public class countryDetailsJson
        {
            public int id { get; set; }
            public string name { get; set; }
            public string iso3 { get; set; }
            public string numeric_code { get; set; }
            public string iso2 { get; set; }
            public string phonecode { get; set; }
            public string capital { get; set; }
            public string currency { get; set; }
            public string currency_name { get; set; }
            public string currency_symbol { get; set; }
            public string tld { get; set; }
            public string native { get; set; }
            public string region { get; set; }
            public int region_id { get; set; }
            public string subregion { get; set; }
            public int subregion_id { get; set; }
            public string nationality { get; set; }
            public string timezones { get; set; }
            public string translations { get; set; }
            public string latitude { get; set; }
            public string longitude { get; set; }
            public string emoji { get; set; }
            public string emojiU { get; set; }
        }


        public class statesJson
        {
            public int id { get; set; }
            public string name { get; set; }
            public string iso2 { get; set; }
        }


        public class stateDetailsJson
        {
            public int id { get; set; }
            public string name { get; set; }
            public int country_id { get; set; }
            public string country_code { get; set; }
            public string iso2 { get; set; }
            public string type { get; set; }
            public string latitude { get; set; }
            public string longitude { get; set; }
        }


        public class citiesJson
        {
            public int id { get; set; }
            public string name { get; set; }
            public string latitude { get; set; }
            public string longitude { get; set; }
        }
    }
}
