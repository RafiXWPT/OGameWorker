using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace Worker.HttpModule
{
    public class MessageContainer
    {
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
                var contentStream = AsyncReadStream(ResponseMessage.Content).Result;
                var doc = new HtmlDocument();
                doc.Load(contentStream);
                ResponseHtmlDocument = doc;
            }
        }

        public HttpRequestMessage RequestMessage { get; }
        public HttpResponseMessage ResponseMessage { get; }
        public HttpStatusCode StatusCode => ResponseMessage.StatusCode;
        public HtmlDocument ResponseHtmlDocument { get; }
        public bool IsHtml => ResponseHtmlDocument != null;

        public static MessageContainer Fail()
        {
            return new MessageContainer(null, new HttpResponseMessage(HttpStatusCode.BadRequest));
        }

        private async Task<Stream> AsyncReadStream(HttpContent content)
        {
            var stream = await content.ReadAsStreamAsync();
            foreach (var encoding in content.Headers.ContentEncoding)
                switch (encoding)
                {
                    case "gzip":
                        stream = new GZipStream(stream, CompressionMode.Decompress);
                        break;
                }

            return stream;
        }
    }
}