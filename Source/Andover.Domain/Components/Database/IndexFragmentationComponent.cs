using System;
using System.Collections.Generic;
using System.Linq;
using Andover.Domain.Components.Database.Provider;
using Andover.Domain.Components.Database.Provider.Entities;
using Andover.Domain.Components.Database.Results;
using Andover.Domain.Core;
using Andover.Domain.Core.Results;

namespace Andover.Domain.Components.Database
{
	public class IndexFragmentationComponent : ComponentBase, IComponent
	{
		private IDatabaseProvider _provider;
		private float _maxIndexFragmentationLevel = 50;
		public List<IndexFragmentationAnalysis> IndexFragmentationResults { get; private set; }

		public IndexFragmentationComponent(IDatabaseProvider provider)
		{
			if (provider == null)
			{
				throw new ArgumentNullException("provider");
			}

			_provider = provider;			
		}

		public override IEnumerable<IComponentResult> Analyze()
		{
			var results = AnalyzeIndexFragmentation();
			return results;
		}

		private List<IndexFragmentationAnalysis> AnalyzeIndexFragmentation()
		{
			IndexFragmentationResults = new List<IndexFragmentationAnalysis>();
			var results = _provider.GetResults<IndexFragmentation>(Queries.IndexFragmentation);
			if (results != null)
			{
				foreach (var result in results)
				{
					var ifa = new IndexFragmentationAnalysis()
					{
						AverageFragmentation = result.AverageFragmentation,
						IndexName = result.Name,
						IsFragmentationPastThreshold = result.AverageFragmentation >= _maxIndexFragmentationLevel
					};

					IndexFragmentationResults.Add(ifa);
				}				
			}
			
			return IndexFragmentationResults;
		}

		public bool IsCompliant
		{
			get
			{
				bool isCompliant = !IndexFragmentationResults.Any(indexFragmentationResult => indexFragmentationResult.IsFragmentationPastThreshold);
				return isCompliant;
			}
		}
	}
}
