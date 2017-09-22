using System.Net.Http;

namespace Worker.HttpModule.Clients
{
    public class OGameHttpClient
    {
        private readonly object _lockObject = new object();
        private readonly HttpClient _httpClient;
        public string Server { get; }
        public string ServerUrl => $"https://{Server}";


        public OGameHttpClient(string server)
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8");
            _httpClient.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");
            _httpClient.DefaultRequestHeaders.Add("Accept-Language", "pl-PL,pl;q=0.8,en-US;q=0.6,en;q=0.4,pt;q=0.2");
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/60.0.3112.113 Safari/537.36");
            Server = server;
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
