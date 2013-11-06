using System.Collections.Generic;
using System.Linq;
using Andover.Domain.Components.Logs.Provider;
using Andover.Domain.Components.Logs.Results;
using Andover.Domain.Core;
using Andover.Domain.Core.Results;

namespace Andover.Domain.Components.Logs
{
	public class ApplicationErrorsComponent : ComponentBase, IComponent
	{
		private ILogsProvider _logsProvider;
		private const string ErrorKeyword = "ERROR";
		private const int ErrorSummaryInstanceThreshold = 10;

		public ApplicationErrorsComponent(ILogsProvider logsProvider) : base()
		{
			_logsProvider = logsProvider;	
		}

		private List<LogEntry> GetErrors()
		{
			return _logsProvider.GetLogEntriesWithKeyword(ErrorKeyword);
		}

		public override IEnumerable<IComponentResult> Analyze()
		{
			var entries = GetErrors();
			var errorSummaries = _logsProvider.GetLogEntrySummaryList(entries);

			return errorSummaries;
		}

		public bool IsCompliant
		{
			get
			{
				bool isPastThreshold = Results<LogEntrySummary>().Any(errorSummary => errorSummary.InstanceCount >= ErrorSummaryInstanceThreshold);
				return !isPastThreshold;
			}
		}
	}
}
