using System;

namespace Andover.Domain.Core.Results.Attributes
{
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	public class ComponentResultName : Attribute
	{
		public string Name { get; private set; }

		public ComponentResultName(string name)
		{
			Name = name;
		}
	}
}
