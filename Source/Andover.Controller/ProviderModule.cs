using Andover.Data.Cache.Provider;
using Andover.Data.Content.Provider;
using Andover.Data.Database.Provider;
using Andover.Data.Logs.Provider;
using Andover.Domain.Components.Cache.Provider;
using Andover.Domain.Components.Content.Provider;
using Andover.Domain.Components.Database.Provider;
using Andover.Domain.Components.Logs.Provider;
using Andover.Domain.Core;
using Ninject;
using Ninject.Modules;

namespace Andover.Controller
{
	public class ProviderModule : NinjectModule
	{
		public AnalyzerSettings AnalyzerSettings { get; private set; }

		public ProviderModule(AnalyzerSettings analyzerSettings)
		{
			AnalyzerSettings = analyzerSettings;
		}

		public SystemSettings GetSystemSettings()
		{
			var systemSettings = new SystemSettings()
			{
				Kernel = new StandardKernel(new ProviderModule(AnalyzerSettings))
			};

			return systemSettings;
		}

		public override void Load()
		{
			Bind<IDatabaseProvider>()
				.To<DatabaseProvider>()
				.WithConstructorArgument("connectionString", AnalyzerSettings.ConnectionString);

			Bind<IContentProvider>()
				.To<ContentProvider>()
				.WithConstructorArgument("databaseName", AnalyzerSettings.DatabaseName)
				.WithConstructorArgument("contentProviderSettings", new ContentProviderSettings(AnalyzerSettings.MaxChildItemsPerParent, AnalyzerSettings.MaxVersionsPerItem, AnalyzerSettings.ContentStartPath));

			Bind<ILogsProvider>()
				.To<LogsProvider>()
				.WithConstructorArgument("logsPath", AnalyzerSettings.LogsPath);

			Bind<ICacheProvider>()
				.To<CacheProvider>();
		}
	}
}
