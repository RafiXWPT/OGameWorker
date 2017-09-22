using System.Net.Http;

namespace Worker.HttpModule.Clients
{
    public class OGameHttpClient
    {
        private readonly object _lockObject = new object();
        private readonly HttpClient _httpClient;
        public string ServerAddress { get; }


        public OGameHttpClient(string serverUrl)
        {
            _httpClient = new HttpClient();
            ServerAddress = serverUrl;
        }

        public HttpResponseMessage SendHttpRequest(HttpRequestMessage message)
        {
           return _httpClient.SendAsync(message).Result;
        }
    }
}
