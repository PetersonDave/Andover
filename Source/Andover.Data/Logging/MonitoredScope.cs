using System;
using Ninject;
using Ninject.Extensions.Logging;

namespace Andover.Data.Logging
{
	public class MonitoredScope : IDisposable
	{
		public ILogger Logger { get; private set; }
		public MonitoredScopeContext Context { get; private set; }

		[Inject]
		public MonitoredScope(ILogger log, MonitoredScopeContext monitoredScopeContext)
		{
			if (log == null)
				throw new ArgumentNullException("log");

			if (monitoredScopeContext == null)
				throw new ArgumentNullException("monitoredScopeContext");

			Logger = log;
			Context = monitoredScopeContext;

			LogScopeStart();
		}

		protected void LogScopeStart()
		{
			Context.ExecutionStartTime = DateTime.Now;
			//Log.Info(string.Format("{0} ({1}) Started .", Context.Id, Context.Name), this);
		}

		protected void LogScopeComplete()
		{
			Context.ExecutionStartTime = DateTime.Now;
			//Log.Info(string.Format("{0} ({1}) Completed.", Context.Id, Context.Name), this);
		}

		public void RecordException(Exception ex)
		{
			//Log.Error(string.Format("{0} ({1}) {2}", Context.Id, Context.Name, ex.Message), ex);
		}

		public void RecordInfo(string message)
		{
			//Log.Info(string.Format("{0} ({1}) {2}", Context.Id, Context.Name, message), this);
		}

		public void RecordWarning(string message)
		{
			//Log.Info(string.Format("{0} ({1}) {2}", Context.Id, Context.Name, message), this);
		}

		public void Dispose()
		{
			LogScopeComplete();
			Logger = null;
		}
	}
}
