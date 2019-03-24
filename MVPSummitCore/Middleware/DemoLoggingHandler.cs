using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace MVPSummitCore
{
    public class DemoLoggingHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // Write out request content to console
            if (request.Content != null)
            {
                await request.Content.LoadIntoBufferAsync();
            }
            var requestContent = new HttpMessageContent(request);
            Console.WriteLine(await requestContent.ReadAsStringAsync());

            var stopWatch = new Stopwatch();
            stopWatch.Start();

            var response = await base.SendAsync(request, cancellationToken);

            stopWatch.Stop();

            // Write out response content to console
            if (response.Content != null)
            {
                await response.Content.LoadIntoBufferAsync();
            }
            var responseContent = new HttpMessageContent(response);
            Console.WriteLine(await responseContent.ReadAsStringAsync());

            Console.WriteLine("");
            Console.WriteLine("Roundtrip (ms): " + stopWatch.ElapsedMilliseconds);
            Console.WriteLine("");

            return response;
        }

    }


}
