using Andover.Domain.Components.Database.Provider.Entities.Attributes;

namespace Andover.Domain.Components.Database.Provider.Entities
{
	public class IndexFragmentation
	{
		[DataField("database_id")]
		public int DatabaseId { get; set; }

		[DataField("OBJECT_ID")]
		public long ObjectId { get; set; }

		[DataField("index_id")]
		public int IndexId { get; set; }

		[DataField("name")]
		public string Name { get; set; }

		[DataField("avg_fragmentation_in_percent")]
		public float AverageFragmentation { get; set; }
	}
}
