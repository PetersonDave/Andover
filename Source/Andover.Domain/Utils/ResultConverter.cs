using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Andover.Domain.Core.Results;
using Andover.Domain.Core.Results.Attributes;

namespace Andover.Domain.Utils
{
	public class ResultConverter
	{
		public static IEnumerable<IEnumerable<Result>> GetResultsFromList(IEnumerable<IComponentResult> listToConvert)
		{
			var convertedList = new List<List<Result>>();

			if (listToConvert != null)
			{
				foreach (IComponentResult componentResult in listToConvert)
				{
					IEnumerable<Result> result = GetResultFromComponentResult(componentResult);
					convertedList.Add(result.ToList());
				}
			}

			return convertedList;
		}

		public static IEnumerable<Result> GetResultFromComponentResult(IComponentResult componentResult)
		{
			var results = new List<Result>();

			Type type = componentResult.GetType();
			PropertyInfo[] infos = type.GetProperties(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
			foreach (var pi in infos)
			{
				var result = new Result();

				var attributes = pi.GetCustomAttributes(typeof (ComponentResultName), false);
				bool isComponentResultName = attributes.Length > 0 && (attributes[0] as ComponentResultName) != null;
				if (isComponentResultName)
				{
					result.Name = (attributes[0] as ComponentResultName).Name;
				}

				attributes = pi.GetCustomAttributes(typeof (ComponentResultDescription), false);
				bool isComponentResultDescription = attributes.Length > 0 && (attributes[0] as ComponentResultDescription) != null;
				if (isComponentResultDescription)
				{
					result.Description = (attributes[0] as ComponentResultDescription).Description;
				}

				result.Value = pi.GetValue(componentResult, null).ToString();
				results.Add(result);
			}

			return results;
		}
	}
}
