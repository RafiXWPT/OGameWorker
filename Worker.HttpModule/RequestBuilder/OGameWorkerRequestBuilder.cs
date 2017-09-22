using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Worker.HttpModule.Clients;
using Worker.HttpModule.Helpers;

namespace Worker.HttpModule.RequestBuilder
{
    public class OGameWorkerRequestBuilder
    {
        private readonly OGameHttpClient _client;
        public OGameWorkerRequestBuilder(OGameHttpClient client)
        {
            _client = client;
        }

        public HttpRequestMessage BuildOverviewRequest()
        {
            var url = $"{_client.ServerAddress}/game/index.php?page=overview";
            return new GetHttpRequest().Create(url);
        }

        public HttpRequestMessage BuildLoginRequest(string username, string password)
        {
            var url = "https://pl.ogame.gameforge.com:443/main/login";
            var content = new Dictionary<string,string>
            {
                {"kid", "" },
                {"login", username},
                {"pass", password},
                {"uni", _client.ServerAddress}
            };

            return new PostHttpRequest().Create(url, content);
        }
    }
}
