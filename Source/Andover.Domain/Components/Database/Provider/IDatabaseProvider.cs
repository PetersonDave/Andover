using System.Collections.Generic;

namespace Andover.Domain.Components.Database.Provider
{
	public interface IDatabaseProvider
	{
		T GetFirstResult<T>(string query);
		IEnumerable<T> GetResults<T>(string query);
	}
}
