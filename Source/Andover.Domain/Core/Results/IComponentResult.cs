using System.Collections.Generic;

namespace Andover.Domain.Core.Results
{
	public interface IComponentResult
	{
		IEnumerable<Result> Normalize();
	}
}