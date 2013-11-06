using Andover.Domain.Core.Results;
using Andover.Domain.Core.Results.Attributes;

namespace Andover.Domain.Components.Database.Results
{
	public class IndexFragmentationAnalysis : ComponentResultBase
	{
		[ComponentResultName("Index Name"),
		ComponentResultDescription("Index Name")]
		public string IndexName { get; set; }

		[ComponentResultName("Average Fragmentation"),
		ComponentResultDescription("Average index fragmentation level")]
		public float AverageFragmentation { get; set; }

		[ComponentResultName("Is Compliant"),
		ComponentResultDescription("Is fragmentation level past threshold")]
		public bool IsFragmentationPastThreshold { get; set; }
	}
}