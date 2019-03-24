//using Microsoft.Graph;
//using Microsoft.Graph.Auth;
using Microsoft.Graph.Auth;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MVPSummit
{
    public class FileBasedTokenStorageProvider : ITokenStorageProvider
    {
        public Task<byte[]> GetTokenCacheAsync(string cacheId)
        {
            if (System.IO.File.Exists($"./tokencache.bin"))
            {
                return Task.FromResult(System.IO.File.ReadAllBytes($"./tokencache.bin"));
            }
            else
            {
                return Task.FromResult(new Byte[0]);
            }
        }

        public Task SetTokenCacheAsync(string cacheId, byte[] tokenCache)
        {
            System.IO.File.WriteAllBytes($"./tokencache.bin", tokenCache);
            return Task.FromResult<object>(null);

        }
    }

    //public static class GraphServiceClientExtensions
    //{
    //    public static T CreateRequest<T>(this GraphServiceClient graphServiceClient, string url, IEnumerable<Option> options = null) where T : BaseRequest
    //    {
    //        var fullUrl = graphServiceClient.BaseUrl + url;
    //        T request = (T)Activator.CreateInstance(typeof(T), new object[] { fullUrl, graphServiceClient, options });
    //        return request;
    //    }
    //}
}
