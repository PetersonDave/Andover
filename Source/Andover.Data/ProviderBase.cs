using System;
using Andover.Data.Logging;
using Ninject;
using Ninject.Extensions.Logging;

namespace Andover.Data
{
	public abstract class ProviderBase : IDisposable
	{
		private MonitoredScope MonitoredScope { get; set; }

		protected ProviderBase()
		{
			InstantiateLogger();
		}

		private void InstantiateLogger()
		{
			var kernel = Utilities.CreateKernel();
			var loggerFactory = kernel.Get<ILoggerFactory>();
			var logger = loggerFactory.GetCurrentClassLogger();
			var context = new MonitoredScopeContext(string.Format("Provider Scope {0}", this.GetType()));
			MonitoredScope = new MonitoredScope(logger, context);			
		}

		public abstract void DisposeProvider();

		public void Dispose()
		{
			MonitoredScope.Dispose();
		}
	}
}
