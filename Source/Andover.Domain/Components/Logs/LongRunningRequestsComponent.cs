using System.Collections.Generic;
using System.Linq;
using Andover.Domain.Components.Logs.Provider;
using Andover.Domain.Components.Logs.Results;
using Andover.Domain.Core;
using Andover.Domain.Core.Results;

namespace Andover.Domain.Components.Logs
{
	public class LongRunningRequestsComponent : ComponentBase, IComponent
	{
		private ILogsProvider _logsProvider;
		private const string LongRunningRequestsKeyword = "Long running operation";
		private const int longRunningRequestsInstanceThreshold = 10;

		public LongRunningRequestsComponent(ILogsProvider logsProvider) : base()
		{
			_logsProvider = logsProvider;
		}

		private List<LogEntry> GetLongRunningRequestsList()
		{
			return _logsProvider.GetLogEntriesWithKeyword(LongRunningRequestsKeyword);
		}

		public override IEnumerable<IComponentResult> Analyze()
		{		
			var entries = GetLongRunningRequestsList();
			var longRunningRequestsList = _logsProvider.GetLogEntrySummaryList(entries);

			return longRunningRequestsList;
		}

		public bool IsCompliant
		{
			get
			{
				bool isPastThreshold = Results<LogEntrySummary>().Any(longRunningRequest => longRunningRequest.InstanceCount >= longRunningRequestsInstanceThreshold);
				return !isPastThreshold;
			}
		}
	}
}
