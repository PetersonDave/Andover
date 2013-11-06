namespace Andover.Data.Content.Provider
{
	public class ContentProviderSettings
	{
		public int MaxChildItemsPerParent { get; private set; }
		public int MaxVersionsPerItem { get; private set; }
		public string StartPath { get; private set; }

		public ContentProviderSettings()
		{
			MaxChildItemsPerParent = 100;   // TODO: Move to config
			MaxVersionsPerItem = 25;        // TODO: Move to config
			StartPath = string.Empty;
		}

		public ContentProviderSettings(int maxChildItemsPerParent, int maxVersionsPerItem, string startPath)
		{
			MaxChildItemsPerParent = maxChildItemsPerParent;
			MaxVersionsPerItem = maxVersionsPerItem;
			StartPath = startPath;
		}
	}
}
