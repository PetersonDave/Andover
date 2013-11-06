using System;
using System.Collections.Generic;
using Andover.Domain.Configuration;
using Andover.Domain.Utils;

namespace Andover.Domain.Core
{
	// review optimization techniques: 
	//	http://sdn.sitecore.net/Articles/Administration/Sitecore%20Performance/Optimizing%20Sitecore%206%20and%20later/Optimizing%20Performance%20in%20Sitecore.aspx
	//	http://sdn.sitecore.net/sdn5/misc/loginpage.aspx?item=%2freference%2fsitecore+6%2foms+performance+tuning+guide&user=extranet%5cAnonymous&login=%2fsdn5%2fmisc%2floginpage.aspx

	public class Analyzer
	{
		public IFactory Factory { get; private set; }
		public List<ICategory> Categories { get; private set; }

		public Analyzer(IFactory factory)
		{
			if (factory == null)
			{
				throw new ArgumentNullException("factory");
			}

			Factory = factory;
			PopulateCategories();
		}

		public void PopulateCategories()
		{
            Categories = new List<ICategory>();

		    foreach (var categorySetting in Factory.ConfigurationSettings)
		    {
                var category = new Category { Name = categorySetting.Key.Name, Description = categorySetting.Key.Description };

                foreach (var componentSetting in categorySetting.Value)
                {
                    var componentType = Type.GetType(componentSetting.Type);
                    var component = Factory.GetComponent(componentType);
                    if (component != null)
                    {
                        component.Name = componentSetting.Name;
                        component.Description = componentSetting.Description;

                        category.Components.Add(component);
                    }
                }

                Categories.Add(category);
 
		    }
		}

		public void Analyze()
		{
			foreach (var category in Categories)
			{
				foreach (var component in category.Components)
				{
					component.RunAnalysis();
				}
			}			
		}

		public void GetComplianceReport()
		{
			foreach (var category in Categories)
			{
			    foreach (var component in category.Components)
			    {
					var componentResults = component.ComplianceReport();
			    }
			}
		}
	}
}
