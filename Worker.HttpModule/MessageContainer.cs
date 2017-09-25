using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace Worker.HttpModule
{
    public class MessageContainer
    {
        public HttpRequestMessage RequestMessage { get; }
        public HttpResponseMessage ResponseMessage { get; }
        public HttpStatusCode StatusCode => ResponseMessage.StatusCode;
        public HtmlDocument ResponseHtmlDocument { get; }
        public bool IsHtml => ResponseHtmlDocument != null;

        public MessageContainer(HttpRequestMessage requestMessage, HttpResponseMessage responseMessage)
        {
            RequestMessage = requestMessage;
            ResponseMessage = responseMessage;
            if (ResponseMessage.Content.Headers.ContentType.MediaType != "text/html")
            {
                ResponseHtmlDocument = null;
            }
            else
            {
                Stream contentStream = AsyncReadStream(ResponseMessage.Content).Result;
                HtmlDocument doc = new HtmlDocument();
                doc.Load(contentStream);
                ResponseHtmlDocument = doc;
            }
        }

        public static MessageContainer Fail() => new MessageContainer(null, new HttpResponseMessage(HttpStatusCode.BadRequest));

        private async Task<Stream> AsyncReadStream(HttpContent content)
        {
            var stream = await content.ReadAsStreamAsync();
            foreach (var encoding in content.Headers.ContentEncoding)
            {
                switch (encoding)
                {
                    case "gzip":
                        stream = new GZipStream(stream, CompressionMode.Decompress);
                        break;
                }
            }

            return stream;
        }
    }
}
