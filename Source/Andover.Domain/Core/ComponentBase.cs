using System;
using System.Collections.Generic;
using System.Linq;
using Andover.Domain.Core.Results;
using Andover.Domain.Utils;

namespace Andover.Domain.Core
{
	public abstract class ComponentBase
	{
		private IEnumerable<IComponentResult> _results;

        public string Name { get; set; }
        public string Description { get; set; }

		public abstract IEnumerable<IComponentResult> Analyze();

		public virtual void Initialize()
		{
			// right now, placeholder.
		}

		public void RunAnalysis()
		{
			Initialize();
			_results = Analyze();
		}

        public IEnumerable<ResultsNormalized> ComplianceReport()
		{
			return _results.Normalize();
		}

		public IEnumerable<TResults> Results<TResults>() where TResults : class
		{
			if (_results == null)
			{
				return new[] {default(TResults)};
			}

			if (!(_results is IEnumerable<TResults>))
			{
				throw new InvalidCastException("Results is not compatible with the given type");
			}

			return _results as IEnumerable<TResults>;			
		}
	}
}
