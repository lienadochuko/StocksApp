using StocksApp.ServiceContract;
using System.Text.Json;

namespace StocksApp.Service
{
    public class FinnHubService : IFinnHubService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        private readonly IConfiguration _configuration;

        public FinnHubService(IHttpClientFactory httpClientFactory
            , IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration; 
        }

        public async Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol)
        {
           using(HttpClient httpClient =
                _httpClientFactory.CreateClient())
            {
                HttpRequestMessage httpRequestMessage =
                    new HttpRequestMessage()
                    {
                        RequestUri = new Uri($"https://finnhub.io/api/v1/quote?symbol={stockSymbol}&token={_configuration["FinnhubToken"]}"),
                        Method = HttpMethod.Get
                    };
               HttpResponseMessage responseMessage =
                   await httpClient.SendAsync(httpRequestMessage);

                Stream stream = responseMessage.Content.ReadAsStream();

                StreamReader streamReader = new StreamReader(stream);

                string reponse = streamReader.ReadToEnd();
                Dictionary<string, object>? responseDictionary = JsonSerializer.Deserialize<Dictionary<string, object>>(reponse);

                if (responseDictionary == null)
                {
                    throw new InvalidOperationException("No response from finnhub server");
                }

                if (responseDictionary.ContainsKey("error"))
                {
                    throw new InvalidOperationException(Convert.ToString(responseDictionary["error"]));
                }



                return responseDictionary;
            }
        }
    }
}
