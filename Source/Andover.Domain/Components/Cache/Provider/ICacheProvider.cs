using System.Collections.Generic;
using Andover.Domain.Components.Cache.Results;

namespace Andover.Domain.Components.Cache.Provider
{
	public interface ICacheProvider
	{
		IEnumerable<CacheDelta> GetAllSitecoreCaches();
	}
}