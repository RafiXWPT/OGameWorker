using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Worker.HttpModule.Helpers
{
    public abstract class BaseHttpRequest
    {
        protected HttpRequestMessage CreateBasicMessage(HttpMethod method, string url)
        {
            var basicMessage = new HttpRequestMessage(method, url);
            basicMessage.Headers.Clear();
            basicMessage.Headers.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8");
            basicMessage.Headers.Add("Accept-Encoding", "gzip, deflate, br");
            basicMessage.Headers.Add("Accept-Language", "pl-PL,pl;q=0.8,en-US;q=0.6,en;q=0.4,pt;q=0.2");
            basicMessage.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/60.0.3112.113 Safari/537.36");
            return basicMessage;
        }

        public abstract HttpRequestMessage Create(string url);
        public abstract HttpRequestMessage Create(string url, Dictionary<string,string> values);
    }
}
