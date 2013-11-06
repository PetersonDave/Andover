using System.Collections.Generic;
using Andover.Domain.Components.Content.Provider;
using Andover.Domain.Components.Content.Results;
using Andover.Domain.Core;

namespace Andover.Data.Content.Provider
{
	public class ContentProvider : ProviderBase, IContentProvider
	{
		private Sitecore.Data.Database _db;
		private ContentProviderSettings _contentProviderSettings;

		private List<ItemCounter> _versionItemsList;
		private List<ItemCounter> _childItemsList;

		public ContentProvider(string databaseName, ContentProviderSettings contentProviderSettings)
		{
			_db = Sitecore.Data.Database.GetDatabase(databaseName);
			_contentProviderSettings = contentProviderSettings;
		}

		public void TraverseContentTree()
		{
			var startItem = _db.GetItem(_contentProviderSettings.StartPath);
			ProcessTree(startItem);
		}

		public List<ItemCounter> GetVersionItemsList()
		{
			if (_versionItemsList == null)
			{
				_versionItemsList = new List<ItemCounter>();
				TraverseContentTree();
			}

			return _versionItemsList;
		}

		public List<ItemCounter> GetChildItemsList()
		{
			if (_childItemsList == null)
			{
				_childItemsList = new List<ItemCounter>();
				TraverseContentTree();
			}

			return _childItemsList;
		}

		private void ProcessTree(Sitecore.Data.Items.Item startItem)
		{
			if (startItem == null) return;

			FindItemsWithLotsOfChildren(startItem);
			FindNumberOfVersionsPerItem(startItem);

			foreach (Sitecore.Data.Items.Item childItem in startItem.Children)
			{
				ProcessTree(childItem);
			}
		}

		private void FindItemsWithLotsOfChildren(Sitecore.Data.Items.Item item)
		{
			if (item == null) return;

			if (item.Children.Count >= _contentProviderSettings.MaxChildItemsPerParent)
			{
				var chlidItem = new ItemCounter()
				{
					Id = item.ID.ToString(),
					Count = item.Children.Count,
					IsCountPastThreshold = item.Children.Count > _contentProviderSettings.MaxChildItemsPerParent
				};

                if (_childItemsList == null)
                {
                    _childItemsList = new List<ItemCounter>();
                }
				_childItemsList.Add(chlidItem);
			}
		}

		private void FindNumberOfVersionsPerItem(Sitecore.Data.Items.Item item)
		{
			if (item == null) return;

			if (item.Versions.Count >= _contentProviderSettings.MaxVersionsPerItem)
			{
				var versionItem = new ItemCounter()
				{
					Id = item.ID.ToString(),
					Count = item.Versions.Count,
					IsCountPastThreshold = item.Versions.Count > _contentProviderSettings.MaxVersionsPerItem
				};

                if (_versionItemsList == null)
                {
                    _versionItemsList = new List<ItemCounter>();
                }
				_versionItemsList.Add(versionItem);
			}
		}

		public override void DisposeProvider()
		{
			throw new System.NotImplementedException();
		}
	}
}