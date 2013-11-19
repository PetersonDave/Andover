using System.Collections.Generic;

namespace Andover.Domain.Components.Database.Provider
{
	public interface IDatabaseProvider
	{
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
		T GetFirstResult<T>(string query);
		IEnumerable<T> GetResults<T>(string query);
	}
}
