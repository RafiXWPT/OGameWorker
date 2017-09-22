using System.Net.Http;

namespace Worker.HttpModule.Clients
{
    public class OGameHttpClient
    {
        private readonly object _lockObject = new object();
        private readonly HttpClient _httpClient;

        public OGameHttpClient()
        {
            _httpClient = new HttpClient();
        }

        public HttpResponseMessage SendHttpRequest(HttpRequestMessage message)
        {
           return _httpClient.SendAsync(message).Result;
        }
    }
}
