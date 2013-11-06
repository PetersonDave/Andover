using System.Collections.Generic;
using System.Linq;
using Andover.Domain.Core.Results;

namespace Andover.Domain.Utils
{
	public static class ComponentResultsExtensions
	{
        public static IEnumerable<ResultsNormalized> Normalize(this IEnumerable<IComponentResult> componentResults)
		{
		    return componentResults.Select(componentResult => new ResultsNormalized(componentResult));
		}
	}
}
