using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Worker.HttpModule.Helpers
{
    internal class PostHttpRequest : BaseHttpRequest
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