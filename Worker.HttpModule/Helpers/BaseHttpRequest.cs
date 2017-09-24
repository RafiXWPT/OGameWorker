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
            return new HttpRequestMessage(method, url);
        }

        public abstract HttpRequestMessage Create(string url);
        public abstract HttpRequestMessage Create(string url, Dictionary<string,string> values);
    }
}
