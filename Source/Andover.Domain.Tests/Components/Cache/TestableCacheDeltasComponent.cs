using System.Collections.Generic;
using Andover.Domain.Components.Cache;
using Andover.Domain.Components.Cache.Provider;
using Andover.Domain.Components.Cache.Results;
using Moq;

namespace Andover.Domain.Tests.Components.Cache
{
	public class TestableCacheDeltasComponent : CacheDeltasComponent
	{
		private List<CacheData> _cacheData = null;

		public TestableCacheDeltasComponent(ICacheProvider provider) : base(provider)
		{
			//_cacheData = cacheData;
		}

		public override IEnumerable<CacheDelta> GetAllSitecoreCaches()
		{
			var caches = new List<CacheDelta>();

			foreach (var cacheData in _cacheData)
			{
				var mock = new Mock<CacheDelta>();
				mock.Setup(ca => ca.MaxSize).Returns(cacheData.MaxSize);
				mock.Setup(ca => ca.RemainingSpace).Returns(cacheData.RemainingSpace);
				mock.Setup(ca => ca.Size).Returns(cacheData.Size);

				caches.Add(mock.Object);
			}

			return caches;
		}
	}
}