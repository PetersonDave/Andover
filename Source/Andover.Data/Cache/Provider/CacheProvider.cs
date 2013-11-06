using System.Collections.Generic;
using System.Linq;
using Andover.Domain.Components.Cache.Provider;
using Andover.Domain.Components.Cache.Results;
using Andover.Domain.Core;
using Sitecore.Caching;

namespace Andover.Data.Cache.Provider
{
    public class CacheProvider : ProviderBase, ICacheProvider
    {
        public override void DisposeProvider()
        {
            // nothing to do, handled by Sitecore.Kernel
        }

        public IEnumerable<CacheDelta> GetAllSitecoreCaches()
        {
            var caches = CacheManager.GetAllCaches();
            var cacheDeltas = (from c in caches
                               where c.MaxSize > 0 && c.RemainingSpace > 0
                               select new CacheDelta()
                {
                    CacheName = c.Name,
                    MaxSize = c.MaxSize,
                    RemainingSpace = c.RemainingSpace,
                    Size = c.Size
                });

            return cacheDeltas;
        }
    }
}
