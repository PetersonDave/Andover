using Ninject;
using Ninject.Extensions.Logging.Log4net;
using Ninject.Modules;

namespace Andover.Data.Logging
{
	public class Utilities
	{
		public static IKernel CreateKernel()
		{
			var settings = CreateSettings();
			return new StandardKernel(settings, new INinjectModule[] { new Log4NetModule() });
		}

		public static INinjectSettings CreateSettings()
		{
			return new NinjectSettings { LoadExtensions = false };
		}
	}
}
