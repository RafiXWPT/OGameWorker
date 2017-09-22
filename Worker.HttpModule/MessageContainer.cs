using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
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
        public Lazy<HtmlDocument> ResponseHtmlDocument { get; }
        public bool IsHtml => ResponseHtmlDocument.Value != null;

        public MessageContainer(HttpRequestMessage requestMessage, HttpResponseMessage responseMessage)
        {
            RequestMessage = requestMessage;
            ResponseMessage = responseMessage;
            ResponseHtmlDocument = new Lazy<HtmlDocument>(() =>
            {
                if (ResponseMessage.Content.Headers.ContentType.MediaType != "text/html")
                {
                    return null;
                }

                Stream contentStream = AsyncReadStream(ResponseMessage.Content).Result;
                HtmlDocument doc = new HtmlDocument();
                doc.Load(contentStream);

                return doc;
            });
        }

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
