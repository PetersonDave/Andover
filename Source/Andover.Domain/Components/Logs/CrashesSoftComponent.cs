using System.Collections.Generic;
using System.Linq;
using Andover.Domain.Components.Logs.Provider;
using Andover.Domain.Components.Logs.Results;
using Andover.Domain.Core;
using Andover.Domain.Core.Results;

namespace Andover.Domain.Components.Logs
{
	public class CrashesSoftComponent : ComponentBase, IComponent
	{
		private ILogsProvider _logsProvider;
		private const string KeywordSoftCrashes = "Sitecore shutting down";
		private const int HardCrashesInstanceThreshold = 10;
		private const int SoftCrashesInstanceThreshold = 10;

		public CrashesSoftComponent(ILogsProvider logsProvider) : base()
		{
			_logsProvider = logsProvider;			
		}

		private List<LogEntry> GetSoftCrashesList()
		{
			return _logsProvider.GetLogEntriesWithKeyword(KeywordSoftCrashes);
		}

		private List<LogEntry> GetHardCrashesList()
		{
			var crashes = _logsProvider.LogEntries.GroupBy(le => le.Filename)
												  .Where(gle => !gle.Any(entry => entry.Message.Contains(KeywordSoftCrashes)))		// entries without the soft crash keyword
												  .Select(gle => gle.LastOrDefault())												// get last log entry before crash
												  .ToList();

			return crashes;
		}

		public override IEnumerable<IComponentResult> Analyze()
		{
			var softCrashesList = GetSoftCrashesList();
			return softCrashesList;
		}

		public bool IsCompliant
		{
			get
			{
				bool isSoftCrashesPastThreshold = Results<LogEntry>().Count() > SoftCrashesInstanceThreshold;
				bool isCompliant = !isSoftCrashesPastThreshold;
				return isCompliant;
			}
		}
	}
}
