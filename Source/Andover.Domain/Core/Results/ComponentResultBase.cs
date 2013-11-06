using System.Collections.Generic;
using Andover.Domain.Utils;

namespace Andover.Domain.Core.Results
{
	//public class ComplianceResults
	//{
	//	public Dictionary<string, IEnumerable<IComponentResult>> Results { get; private set; }

	//	public void Add(string key, IEnumerable<IComponentResult> complianceResults)
	//	{
	//		Results.Add(key, complianceResults);
	//	}

	//	public IEnumerable<IComponentResult> FindByKey(string description)
	//	{
	//		var theList = Results.SingleOrDefault(result => string.Compare(result.Description, description, StringComparison.OrdinalIgnoreCase) == 0);
	//		return theList == null ? null : theList.Results;
	//	}
	//}

	public class ComponentResultBase : IComponentResult
	{
		public IEnumerable<Result> Normalize()
		{
			return ResultConverter.GetResultFromComponentResult(this);
		}
	}
}