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
        private readonly Dictionary<string, string> _basicHeaders = new Dictionary<string,string>
        {
            {"Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8"},
            {"Accept-Encoding", "gzip, deflate, br"},
            {"Accept-Language", "pl-PL,pl;q=0.8,en-US;q=0.6,en;q=0.4,pt;q=0.2"},
            {"User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/60.0.3112.113 Safari/537.36"},
        };

        protected void RegisterDefaultHeaders(HttpRequestMessage message)
        {
            foreach (var header in _basicHeaders)
            {
                message.Headers.Add(header.Key, header.Value);
            }
        }

        protected HttpRequestMessage CreateBasicMessage(HttpMethod method, string url)
        {
            var basicMessage = new HttpRequestMessage(method, url);
            RegisterDefaultHeaders(basicMessage);
            return basicMessage;
        }

        public abstract HttpRequestMessage BuildRequestMessage(string url, Dictionary<string,string> values);
    }
}
