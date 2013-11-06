using System.Collections.Generic;
using System.Linq;
using Andover.Domain.Components.Content.Provider;
using Andover.Domain.Components.Content.Results;
using Andover.Domain.Components.Database.Results;
using Andover.Domain.Core;
using Andover.Domain.Core.Results;

namespace Andover.Domain.Components.Content
{
	public class ItemVersionsComponent : ComponentBase, IComponent
	{
		private IContentProvider _contentProvider;

		public ItemVersionsComponent(IContentProvider contentProvider)
		{
			_contentProvider = contentProvider;
		}

		public override IEnumerable<IComponentResult> Analyze()
		{
            // analysis done in provider via Sitecore.Kernel
			return _contentProvider.GetVersionItemsList();
		}

		public bool IsCompliant
		{
			get
			{
				bool isCompliant = !Results<ItemCounter>().Any(ci => ci.IsCountPastThreshold);
				return isCompliant;
			}
		}
	}
}
