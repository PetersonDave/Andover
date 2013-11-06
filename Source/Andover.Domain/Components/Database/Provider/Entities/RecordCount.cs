using Andover.Domain.Components.Database.Provider.Entities.Attributes;

namespace Andover.Domain.Components.Database.Provider.Entities
{
	public class RecordCount
	{
		[DataField("RowCountValue")]
		public int Count { get; set; }
	}
}
