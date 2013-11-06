using System;

namespace Andover.Domain.Exceptions
{
	public class ProviderNotMappedException : Exception
	{
		public ProviderNotMappedException(string providerName) : base(ModifyErrorMessage(providerName)) { }

		private static string ModifyErrorMessage(string providerName)
		{
			return string.Format("Provider {0} is not defined within Provider Container. Map your provider within Andover.ProviderModule.Load", providerName);
		}
	}
}
