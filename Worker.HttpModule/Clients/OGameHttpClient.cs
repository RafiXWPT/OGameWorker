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

        public MessageContainer SendHttpRequest(HttpRequestMessage request)
        {
            return SendHttpRequestInternal(request);
        }

        private MessageContainer SendHttpRequestInternal(HttpRequestMessage request)
        {
            lock (_lockObject)
            {
                var response = _httpClient.SendAsync(request).Result;
                return new MessageContainer(request, response);
            }
        }
    }
}
