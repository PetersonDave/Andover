using System;
using Andover.Domain.Core.Results;
using Andover.Domain.Core.Results.Attributes;

namespace Andover.Domain.Components.Logs.Results
{
	public class LogEntry : ComponentResultBase
	{
		[ComponentResultName("Filename"),
		ComponentResultDescription("Log filename")]
		public string Filename { get; set; }

		[ComponentResultName("Date Time"),
		ComponentResultDescription("Time of log entry")]
		public DateTime Timestamp { get; set; }

		[ComponentResultName("Message"),
		ComponentResultDescription("Log message text")]
		public string Message { get; set; }
	}
}