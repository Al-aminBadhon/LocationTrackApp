using GetLocationApp.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace GetLocationApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly GeoLocationHelper _geoLocationHelper;
        public HomeController(ILogger<HomeController> logger, GeoLocationHelper geoLocationHelper)
        {
            _logger = logger;
            _geoLocationHelper = geoLocationHelper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public async Task<IActionResult> GeoInformation() 
        {
            string json = await _geoLocationHelper.GetGeoInfo();

            
            JsonGeoToViewModel model = new JsonGeoToViewModel();

            model = JsonConvert.DeserializeObject<JsonGeoToViewModel>(json);

            // or we can get into a dynamic variable
            dynamic data = JObject.Parse(json);
            string CountryName = data.country_name;
            TempData["CountryName"] = CountryName;
            return View(model);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}