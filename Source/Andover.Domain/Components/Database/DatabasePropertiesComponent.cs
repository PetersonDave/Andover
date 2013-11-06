using System;
using System.Collections.Generic;
using System.Linq;
using Andover.Domain.Components.Database.Provider;
using Andover.Domain.Components.Database.Provider.Entities;
using Andover.Domain.Components.Database.Results;
using Andover.Domain.Core;
using Andover.Domain.Core.Results;

namespace Andover.Domain.Components.Database
{
	public class DatabasePropertiesComponent : ComponentBase, IComponent
	{
		private IDatabaseProvider _provider;
        public List<DatabasePropertiesAnalysis> DatabasePropertiesResults { get; private set; }

		public DatabasePropertiesComponent(IDatabaseProvider provider)
		{
			if (provider == null)
			{
				throw new ArgumentNullException("provider");
			}

			_provider = provider;
		}

		public override IEnumerable<IComponentResult> Analyze()
		{
			var results = AnalyzeDatabaseProperties();
			return results;
		}

		private List<DatabasePropertiesAnalysis> AnalyzeDatabaseProperties()
		{
            DatabasePropertiesResults = new List<DatabasePropertiesAnalysis>();

			var databaeProperties = _provider.GetResults<DatabaseProperties>(Queries.DatabaseProperties);
            if (databaeProperties != null)
			{
			    foreach (var databaseProperty in databaeProperties)
			    {
			        bool isAutoshrink;
			        bool.TryParse(databaseProperty.AutoShrink, out isAutoshrink);

                    bool isAutoClose;
			        bool.TryParse(databaseProperty.AutoClose, out isAutoClose);

                    bool failsValidation = isAutoshrink || isAutoClose || string.Compare(databaseProperty.RecoveryModel, "simple", StringComparison.OrdinalIgnoreCase) == 0;

                    DatabasePropertiesResults.Add(new DatabasePropertiesAnalysis
                        {
                            AutoClose = isAutoClose,
                            AutoShrink = isAutoshrink,
                            CompatabilityLevel = databaseProperty.CompatabilityLevel,
                            DatabaseName = databaseProperty.DatabaseName,
                            RecoveryModel = databaseProperty.RecoveryModel,
                            IsPropertySetCompliant = !failsValidation
                        });
			    }
			}

            return DatabasePropertiesResults;
		}

		public bool IsCompliant
		{
			get 
            {
                bool isCompliant = !DatabasePropertiesResults.Any(indexFragmentationResult => indexFragmentationResult.IsPropertySetCompliant);
                return isCompliant;
            }
		}
	}
}
