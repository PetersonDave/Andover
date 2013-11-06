using System.Collections.Generic;
using Andover.Domain.Core.Results;

namespace Andover.Domain.Core
{
	public interface IComponent
	{
        string Name { get; set; }
        string Description { get; set; }
		bool IsCompliant { get; }
		IEnumerable<TResults> Results<TResults>() where TResults : class;
        IEnumerable<ResultsNormalized> ComplianceReport();

		void Initialize();
		void RunAnalysis();
		IEnumerable<IComponentResult> Analyze();
	}
}
