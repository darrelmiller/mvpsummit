using Microsoft.Graph;
using System;
using System.IO.Compression;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace MVPSummitCore
{
    public class CompressionHandler : DelegatingHandler
    {
        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var gzipQString = new StringWithQualityHeaderValue("gzip");
            if (!request.Headers.AcceptEncoding.Contains(gzipQString))
            {
                request.Headers.AcceptEncoding.Add(gzipQString);
            }

            var requestContext = request.GetRequestContext();
            requestContext.FeatureUsage |= FeatureFlag.CompressionHandler;

            if (request.Content != null && request.Content.Headers.ContentLength > 1024)
            {
                request.Content = new CompressedContent(request.Content, "gzip");
            }

            var response = await base.SendAsync(request, cancellationToken);

            if (response.Content != null && response.Content.Headers.ContentEncoding.Contains("gzip"))
            {
                response.Content = new StreamContent(new GZipStream(await response.Content.ReadAsStreamAsync(), CompressionMode.Decompress));
            }
            return response;
        }
    }


}
