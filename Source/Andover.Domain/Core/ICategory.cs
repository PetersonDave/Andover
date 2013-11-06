using System.Collections.Generic;

namespace Andover.Domain.Core
{
	public interface ICategory
	{
		string Name { get; }
		string Description { get; }
		List<IComponent> Components { get; }

		bool IsCompliant { get; }
	}
}