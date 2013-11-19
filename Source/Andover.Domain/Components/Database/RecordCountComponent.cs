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
	public class RecordCountComponent : ComponentBase, IComponent
	{
		private IDatabaseProvider _provider;
		private const int _maxEventQueueSize = 100;
		private const int _maxHistorySize = 100;
		private const int _maxPublishQueueSize = 100;
		public List<RecordCountAnalysis> RecordCountResults { get; private set; }

		public RecordCountComponent(IDatabaseProvider provider)
		{
			if (provider == null)
			{
				throw new ArgumentNullException("provider");
			}

			_provider = provider;
			RecordCountResults = new List<RecordCountAnalysis>();
		}

		public override IEnumerable<IComponentResult> Analyze()
		{
			AnalyzeEventQueue();
			AnalyzeHistory();
			AnalyzePublishQueue();

			return RecordCountResults;
		}

		private void AnalyzeEventQueue()
		{
		    var query = string.Format(Queries.CountEventQueue, _provider.DatabaseName);
            var rca = GetRecordCountAnalysis(query, _maxEventQueueSize);
			if (rca != null)
			{
				rca.TableName = "EventQueue";
				RecordCountResults.Add(rca);		
			}
		}

		private void AnalyzeHistory()
		{
            var query = string.Format(Queries.CountHistory, _provider.DatabaseName);
            var rca = GetRecordCountAnalysis(query, _maxHistorySize);
			if (rca != null)
			{
				rca.TableName = "History";
				RecordCountResults.Add(rca);
			}
		}

		private void AnalyzePublishQueue()
		{
            var query = string.Format(Queries.CountPublishQueue, _provider.DatabaseName);
            var rca = GetRecordCountAnalysis(query, _maxPublishQueueSize);
			if (rca != null)
			{
				rca.TableName = "PublishQueue";
				RecordCountResults.Add(rca);
			}
		}

		private RecordCountAnalysis GetRecordCountAnalysis(string query, int maxSize)
		{
			RecordCountAnalysis rca = null;
			var result = _provider.GetFirstResult<RecordCount>(query);
			if (result != null)
			{
				rca = new RecordCountAnalysis()
				{
					Count = result.Count,
					IsCountPastThreshold = result.Count >= maxSize
				};
			}

			return rca;
		}

		public bool IsCompliant
		{
			get
			{
				bool isCompliant = !RecordCountResults.Any(recordCountAnalysis => recordCountAnalysis.IsCountPastThreshold);
				return isCompliant;
			}
		}
	}
}
