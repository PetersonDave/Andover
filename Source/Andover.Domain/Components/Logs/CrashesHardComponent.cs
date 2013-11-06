using System.Collections.Generic;
using System.Linq;
using Andover.Domain.Components.Logs.Provider;
using Andover.Domain.Components.Logs.Results;
using Andover.Domain.Core;
using Andover.Domain.Core.Results;

namespace Andover.Domain.Components.Logs
{
	public class CrashesHardComponent : ComponentBase, IComponent
	{
		private ILogsProvider _logsProvider;
		private const string KeywordSoftCrashes = "Sitecore shutting down";
		private const int HardCrashesInstanceThreshold = 10;

		public CrashesHardComponent(ILogsProvider logsProvider) : base()
		{
			_logsProvider = logsProvider;			
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
			var softCrashesList = GetHardCrashesList();
			return softCrashesList;
		}

		public bool IsCompliant
		{
			get
			{
				bool isHardCrashesPastThreshold = Results<LogEntry>().Count() > HardCrashesInstanceThreshold;
				bool isCompliant = !isHardCrashesPastThreshold;
				return isCompliant;
			}
		}
	}
}
