using System;

namespace Andover.Domain.Core.Results.Attributes
{
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	public class ComponentResultDescription : Attribute
	{
		public string Description { get; private set; }

		public ComponentResultDescription(string description)
		{
			Description = description;
		}
	}
}