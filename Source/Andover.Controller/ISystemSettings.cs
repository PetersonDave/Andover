using System;
using System.Collections.Generic;
using Ninject;

namespace Andover.Controller
{
	public interface ISystemSettings
	{
		IKernel Kernel { get; set; }
		Dictionary<Type, Object> Providers { get; set; }
	}
}