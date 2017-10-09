using System.Collections.Generic;
using System.Net.Http;

namespace Worker.HttpModule.Helpers
{
    public abstract class BaseHttpRequest
    {
        protected HttpRequestMessage CreateBasicMessage(HttpMethod method, string url)
        {
            var request = new HttpRequestMessage(method, url);
            request.Headers.Add("Cache-Control", "max-age=0");
            request.Headers.Add("Upgrade-Insecure-Requests", "1");
            request.Headers.Add("Origin", "https://s147-pl.ogame.gameforge.com");
            return request;
        }

        public abstract HttpRequestMessage Create(string url);
        public abstract HttpRequestMessage Create(string url, Dictionary<string, string> values);
    }
}