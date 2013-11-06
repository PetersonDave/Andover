using System;
using System.Collections.Generic;
using Ninject;

namespace Andover.Controller
{
	public class SystemSettings : ISystemSettings
	{
		public IKernel Kernel { get; set; }
		public Dictionary<Type, Object> Providers { get; set; }

		public SystemSettings()
		{
			Providers = new Dictionary<Type, Object>();
		}
	}
}