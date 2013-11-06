using Andover.Domain.Components.Database.Provider.Entities.Attributes;

namespace Andover.Domain.Components.Database.Provider.Entities
{
	public class DatabaseProperties
	{
		[DataField("DatabaseName")]
		public string DatabaseName { get; set; }

		[DataField("compatibility_level")]
		public string CompatabilityLevel { get; set; }

		[DataField("AutoShrink")]
		public string AutoShrink { get; set; }			// TODO: string value of "0" or "1" fails conversion to bool

		[DataField("AutoClose")]
		public string AutoClose { get; set; }			// TODO: string value of "0" or "1" fails conversion to bool

		[DataField("Recovery Model")]
		public string RecoveryModel { get; set; }
	}
}
