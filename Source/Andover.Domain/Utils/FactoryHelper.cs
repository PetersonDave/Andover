using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Andover.Domain.Utils
{
	public class FactoryHelper
	{
		public static List<Type> GetClasses(Type baseType)
		{
			return Assembly.GetExecutingAssembly().GetTypes().Where(type => type.IsSubclassOf(baseType)).ToList();
		}
	}
}
