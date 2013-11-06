using System;

namespace Andover.Domain.Utils
{
	public class DateFormatting
	{
		public static Func<string, DateTime?> TryParseDate = value =>
		{
			DateTime dateValue;
			return DateTime.TryParse(value, out dateValue) ? (DateTime?)dateValue : null;
		};

	}
}
