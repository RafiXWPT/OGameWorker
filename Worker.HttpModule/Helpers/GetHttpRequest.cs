using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Worker.HttpModule.Helpers
{
    class GetHttpRequest : BaseHttpRequest
    {
        public override HttpRequestMessage BuildRequestMessage(string url, Dictionary<string, string> values)
        {
            var requestMessage = CreateBasicMessage(HttpMethod.Get, url);
            return requestMessage;
        }
    }
}
