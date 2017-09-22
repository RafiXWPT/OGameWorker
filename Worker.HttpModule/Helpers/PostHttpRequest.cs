using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Worker.HttpModule.Helpers
{
    class PostHttpRequest : BaseHttpRequest
    {
        public override HttpRequestMessage Create(string url)
        {
            throw new NotImplementedException();
        }

        public override HttpRequestMessage Create(string url, Dictionary<string, string> content)
        {
            var requestMessage = CreateBasicMessage(HttpMethod.Post, url);
            requestMessage.Content = new FormUrlEncodedContent(content);
            return requestMessage;
        }
    }
}
