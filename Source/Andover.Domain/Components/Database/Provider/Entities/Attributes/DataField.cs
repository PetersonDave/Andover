using System;

namespace Andover.Domain.Components.Database.Provider.Entities.Attributes
{
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	sealed public class DataField : Attribute
	{
		public string FieldName { get; private set; }

		public DataField(string fieldName)
		{
			FieldName = fieldName;
		}
	}
}