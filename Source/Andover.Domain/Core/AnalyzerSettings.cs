namespace Andover.Domain.Core
{
	public class AnalyzerSettings
	{
		public string ConnectionString { get; set; }
		public string DatabaseName { get; set; }
		public string LogsPath { get; set; }
        public int MaxChildItemsPerParent { get; set; }
        public int MaxVersionsPerItem { get; set; }
        public string ContentStartPath { get; set; }
	}
}