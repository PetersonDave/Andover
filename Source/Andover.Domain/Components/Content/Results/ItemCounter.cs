using Andover.Domain.Core.Results;
using Andover.Domain.Core.Results.Attributes;

namespace Andover.Domain.Components.Content.Results
{
	public class ItemCounter : ComponentResultBase
	{
		[ComponentResultName("Sitecore Id"),
		ComponentResultDescription("Id of Sitecore item")]
		public string Id { get; set; }

		[ComponentResultName("Count"),
		ComponentResultDescription("Number of maching items found")]
		public int Count { get; set; }

		[ComponentResultName("Is Past Threshold"),
		ComponentResultDescription("Is Past Threshold")]
		public bool IsCountPastThreshold { get; set; }

	}
}