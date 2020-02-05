using System;
using System.Threading.Tasks;
using Microsoft.Graph.Auth;

namespace MVPSummitCore
{
    public class FileBasedTokenStorageProvider //: ITokenStorageProvider
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


}
