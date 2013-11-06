using System.Collections.Generic;
using Andover.Domain.Components.Content.Results;

namespace Andover.Domain.Components.Content.Provider
{
	public interface IContentProvider
	{
		void TraverseContentTree();

		List<ItemCounter> GetVersionItemsList();
		List<ItemCounter> GetChildItemsList();
	}
}
