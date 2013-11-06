using System;

namespace Andover.Data.Logging
{
	public class MonitoredScopeContext
	{
		public Guid Id { get; private set; }
		public string Name { get; set; }
		public DateTime ExecutionStartTime { get; set; }

		public MonitoredScopeContext(string scopeName)
		{
			Name = scopeName;
			Id = Guid.NewGuid();
		}
	}
}