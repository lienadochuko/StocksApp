using Microsoft.AspNetCore.Mvc;
using StocksApp.Service;

namespace StocksApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly FinnHubService _finnHubService;
        public HomeController(FinnHubService finnHub)
        {
            _finnHubService = finnHub;
        }

        [Route("/")]
        public async Task<IActionResult> Index()
        {
           Dictionary<string, object> responseDictionary =
                await _finnHubService.GetStockPriceQuote("AAPL");
            return View();
        }
    }
}
