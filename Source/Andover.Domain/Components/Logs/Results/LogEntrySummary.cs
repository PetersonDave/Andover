using Andover.Domain.Core.Results;
using Andover.Domain.Core.Results.Attributes;

namespace Andover.Domain.Components.Logs.Results
{
	public class LogEntrySummary : ComponentResultBase, IComponentResult
	{
		[ComponentResultName("Message"),
		ComponentResultDescription("Log message text")]
		public string Message { get; set; }

		[ComponentResultName("Count"),
		ComponentResultDescription("Count of message instances")]
		public int InstanceCount { get; set; }
	}
}