using Andover.Domain.Core.Results;
using Andover.Domain.Core.Results.Attributes;

namespace Andover.Domain.Components.Database.Results
{
	public class RecordCountAnalysis : ComponentResultBase
	{
        [ComponentResultName("Table Name"),
        ComponentResultDescription("Table Name")]
        public string TableName { get; set; }

        [ComponentResultName("Count"),
        ComponentResultDescription("Count")]        
        public int Count { get; set; }

        [ComponentResultName("Is Compliant"),
        ComponentResultDescription("Is record count past threshold")]
        public bool IsCountPastThreshold { get; set; }
	}
}