using System.Collections.Generic;
using System.IO;
using System.Linq;
using Andover.Data.Logs.Provider.Chainsaw;
using Andover.Data.Logs.Provider.Chainsaw.Extensions;
using Andover.Domain.Components.Logs.Provider;
using Andover.Domain.Components.Logs.Results;

namespace Andover.Data.Logs.Provider
{
	public class LogsProvider : ProviderBase, ILogsProvider
	{
		public List<LogEntry> LogEntries { get; set; }
		public int LogFilesCount { get; private set; }

		public virtual string LogsPath { get; private set; }

		public LogsProvider(string logsPath)
		{
            LogsPath = logsPath;
			LogEntries = GetLogEntriesList();
		}

		private List<LogEntry> GetLogEntriesList()
		{
			var logEntryList = new List<LogEntry>();
			var di = new DirectoryInfo(LogsPath);
			var logs = di.GetFiles("*.txt", SearchOption.TopDirectoryOnly);

			LogFilesCount = logs.Count();

			foreach (var log in logs)
			{
				var logEntries = LogParser.GetLogFileContents(log, LogsPath);
				logEntryList.AddRange(logEntries);
			}

			return logEntryList;
		}

		public List<LogEntry> GetLogEntriesWithKeyword(string keyword)
		{
			return LogEntries.GetLogEntriesWithKeyword(keyword);
		}

		public List<LogEntry> GetLogEntriesWithoutKeyword(string keyword)
		{
			return LogEntries.GetLogEntriesWithoutKeyword(keyword);
		}

		// TODO: Fix this!
		public IEnumerable<LogEntrySummary> GetLogEntrySummaryList(List<LogEntry> logEntries)
		{
			return logEntries.GetLogEntrySummaryList();
		}

		public override void DisposeProvider()
		{
			throw new System.NotImplementedException();
		}
	}
}
