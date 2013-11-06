using System.Collections.Generic;
using System.Linq;
using Andover.Domain.Components.Cache.Provider;
using Andover.Domain.Components.Cache.Results;
using Andover.Domain.Core;
using Andover.Domain.Core.Results;

namespace Andover.Domain.Components.Cache
{
	public class CacheDeltasComponent : ComponentBase, IComponent
	{
		private ICacheProvider _provider;
		private int _deltaThresholdPercentage = 80;
		public int DeltaThresholdPercentage { get { return _deltaThresholdPercentage; } }

		public CacheDeltasComponent(ICacheProvider provider)
		{
			_provider = provider;
		}

		public override IEnumerable<IComponentResult> Analyze()
		{
			var cacheDeltas = GetAllSitecoreCaches();
			return cacheDeltas;
		}

		public virtual IEnumerable<CacheDelta> GetAllSitecoreCaches()
		{
			var results = _provider.GetAllSitecoreCaches();

            // apply business rule for determining caceh delta threshold
		    var allSitecoreCaches = results as IList<CacheDelta> ?? results.ToList();
		    allSitecoreCaches.ToList().ForEach(result => result.IsDeltaPastThreshold = (result.Size / result.MaxSize) * 100 >= _deltaThresholdPercentage);

		    return allSitecoreCaches;
		}

		public bool IsCompliant
		{
			get
			{
				bool isCompliant = !Results<CacheDelta>().Any(cacheDelta => cacheDelta.IsDeltaPastThreshold);
				return isCompliant;
			}
		}
	}
}
