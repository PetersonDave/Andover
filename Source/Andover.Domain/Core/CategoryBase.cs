using System.Collections.Generic;
using System.Linq;

namespace Andover.Domain.Core
{
	public class Category : ICategory
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public List<IComponent> Components { get; set; }

		public bool IsCompliant
		{
			get
			{
				bool isCompliant = Components.Any(component => component.IsCompliant);
				return isCompliant;
			}
		}

		public Category()
		{
			Components = new List<IComponent>();
		}
	}
}
