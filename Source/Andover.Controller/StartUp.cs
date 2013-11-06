using Andover.Domain.Core;

namespace Andover.Controller
{
	public class StartUp
	{
		public static SystemSettings GetSystemSettings(AnalyzerSettings analyzerSettings)
		{
			var provider = new ProviderModule(analyzerSettings);
			return provider.GetSystemSettings();
		}
	}
}
