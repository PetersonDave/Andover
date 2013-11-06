using Andover.Domain.Core.Results;
using Andover.Domain.Core.Results.Attributes;

namespace Andover.Domain.Components.Cache.Results
{
	public class CacheDelta : ComponentResultBase
	{
		[ComponentResultName("Size"),
		ComponentResultDescription("Cache delta size")]
		public long Size { get; set; }

		[ComponentResultName("Maximum Size"),
		ComponentResultDescription("Maximum size of cache delta")]
		public long MaxSize { get; set; }

		[ComponentResultName("Remaining Space"),
		ComponentResultDescription("Remaining space before reaching threshold")]
		public long RemainingSpace { get; set; }

		[ComponentResultName("Beyond Threshold"),
		 ComponentResultDescription("Determines if cache delta is beyond threshold")]
		public bool IsDeltaPastThreshold { get; set; }

        [ComponentResultName("Cache Name"),
         ComponentResultDescription("Cache Setting Name")]
	    public string CacheName { get; set; }
	}
}