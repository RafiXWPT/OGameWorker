using System.Collections.Generic;
using System.Net.Http;

namespace Worker.HttpModule.Helpers
{
    internal class GetHttpRequest : BaseHttpRequest
    {
        public override HttpRequestMessage Create(string url)
        {
            return CreateBasicMessage(HttpMethod.Get, url);
        }

        public override HttpRequestMessage Create(string url, Dictionary<string, string> content)
        {
            var requestMessage = CreateBasicMessage(HttpMethod.Get, url);
            requestMessage.Headers.Add("Upgrade-Insecure-Requests", "1");
            return requestMessage;
        }
    }
}