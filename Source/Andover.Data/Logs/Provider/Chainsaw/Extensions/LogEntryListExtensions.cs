using System.Collections.Generic;
using System.Linq;
using Andover.Domain.Components.Logs.Results;

namespace Andover.Data.Logs.Provider.Chainsaw.Extensions
{
	public static class LogEntryListExtensions
	{
		public static List<LogEntry> GetLogEntriesWithKeyword(this List<LogEntry> logEntries, string keyword)
		{
			return logEntries.Where(entry => entry.Message.Contains(keyword)).DefaultIfEmpty().ToList();
		}

		public static List<LogEntry> GetLogEntriesWithoutKeyword(this List<LogEntry> logEntries, string keyword)
		{
			return logEntries.Where(entry => !entry.Message.Contains(keyword)).DefaultIfEmpty().ToList();
		}

		public static IEnumerable<LogEntrySummary> GetLogEntrySummaryList(this List<LogEntry> entries)
		{
			var summaryEntries = entries.GroupBy(entry => entry.Message.Substring(0, entry.Message.IndexOf("\n") >= 0 ? entry.Message.IndexOf("\n") : entry.Message.Length))
			                            .OrderByDescending(entry => entry.Count())
			                            .Select(entry => new LogEntrySummary()
				                            {
					                            InstanceCount = entry.Count(),
					                            Message = entry.Key.Trim()
				                            }).ToList();
			return summaryEntries;
		}
	}
}