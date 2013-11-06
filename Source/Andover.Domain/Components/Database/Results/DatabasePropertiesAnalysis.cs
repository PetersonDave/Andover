using Andover.Domain.Core.Results;
using Andover.Domain.Core.Results.Attributes;

namespace Andover.Domain.Components.Database.Results
{
    public class DatabasePropertiesAnalysis : ComponentResultBase
    {
        [ComponentResultName("DatabaseName"),
         ComponentResultDescription("DatabaseName")]
        public string DatabaseName { get; set; }

        [ComponentResultName("CompatabilityLevel"),
         ComponentResultDescription("CompatabilityLevel")]
        public string CompatabilityLevel { get; set; }

        [ComponentResultName("AutoShrink"),
         ComponentResultDescription("AutoShrink")]
        public bool AutoShrink { get; set; }

        [ComponentResultName("AutoClose"),
         ComponentResultDescription("AutoClose")]
        public bool AutoClose { get; set; }

        [ComponentResultName("RecoveryModel"),
         ComponentResultDescription("RecoveryModel")]
        public string RecoveryModel { get; set; }

        [ComponentResultName("Is Compliant"),
        ComponentResultDescription("Do properties match recommended settings")]
        public bool IsPropertySetCompliant { get; set; }
    }
}