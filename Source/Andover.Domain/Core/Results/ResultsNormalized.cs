using System.Collections.Generic;
using System.Linq;

namespace Andover.Domain.Core.Results
{
    public class ResultsNormalized
    {
        public IEnumerable<Result> Results { get; private set; }

        public ResultsNormalized(IComponentResult componentResult)
        {
            Results = componentResult.Normalize().ToList();
        }
    }
}