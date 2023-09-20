using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using StocksApp.Models;
using StocksApp.Service;

namespace StocksApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly FinnHubService _finnHubService;

        private readonly IOptions<TradingOption> _options;
        public HomeController(FinnHubService finnHub, IOptions<TradingOption> options)
        {
            _finnHubService = finnHub;
            
            _options = options;
        }

        [Route("/")]
        public async Task<IActionResult> Index()
        {
            if(_options.Value.Default == null)
            {
                _options.Value.Default = "MSFT";
            }
           Dictionary<string, object> responseDictionary =
                await _finnHubService.GetStockPriceQuote(_options.Value.Default);

            Stock stock = new Stock() { 
                StockSymbol = _options.Value.Default.ToString(),
                CurrentPrice = Convert.ToDouble(responseDictionary["c"].ToString()), 
                HighestPrice = Convert.ToDouble(responseDictionary["h"].ToString()),
                LowestPrice = Convert.ToDouble(responseDictionary["l"].ToString()),
                OpenPrice = Convert.ToDouble(responseDictionary["o"].ToString()),
            };

            return View(stock);
        }
    }
}
