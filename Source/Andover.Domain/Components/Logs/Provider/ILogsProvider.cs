using System.Collections.Generic;
using Andover.Domain.Components.Logs.Results;

namespace Andover.Domain.Components.Logs.Provider
{
	public interface ILogsProvider
	{
		List<LogEntry> LogEntries { get; set; }
		List<LogEntry> GetLogEntriesWithKeyword(string keyword);
		List<LogEntry> GetLogEntriesWithoutKeyword(string keyword);
		IEnumerable<LogEntrySummary> GetLogEntrySummaryList(List<LogEntry> logEntries);
	}
}