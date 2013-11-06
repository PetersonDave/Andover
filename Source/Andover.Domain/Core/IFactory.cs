using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Andover.Domain.Configuration;

namespace Andover.Domain.Core
{
	public interface IFactory
	{
        Dictionary<ICategoryDto, List<IComponentDto>> ConfigurationSettings { get; set; }
		void PreloadProviders(Type T);
		List<ICategory> GetRelatedCategories<TCategory>(IFactory factory);
		IComponent GetComponent(Type T);
	}
}
