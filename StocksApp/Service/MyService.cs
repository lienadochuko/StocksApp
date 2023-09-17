using System.Text.Json;

namespace StocksApp.Service
{
    public class MyService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public MyService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task method()
        {
           using(HttpClient httpClient =
                _httpClientFactory.CreateClient())
            {
                HttpRequestMessage httpRequestMessage =
                    new HttpRequestMessage()
                    {
                        RequestUri = new Uri("https://finnhub.io/api/v1/quote?symbol=AAPL&token=ck32cu9r01qp0k7688f0ck32cu9r01qp0k7688fg"),
                        Method = HttpMethod.Get
                    };
               HttpResponseMessage responseMessage =
                   await httpClient.SendAsync(httpRequestMessage);

                Stream stream = responseMessage.Content.ReadAsStream();

                StreamReader streamReader = new StreamReader(stream);

                string reponse = streamReader.ReadToEnd();
                Dictionary<string, object>? responseDictionary = JsonSerializer.Deserialize<Dictionary<string, object>>(reponse);
            }
        }
    }
}
