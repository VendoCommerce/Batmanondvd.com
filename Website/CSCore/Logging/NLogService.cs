using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;


namespace CSCore.Logging
{
	public class NLogService : ILogger
	{
		static Logger _loggerInstance;
		static NLogService()
		{
			_loggerInstance = LogManager.GetLogger("");
		}

		public void LogException(string message, Exception ex)
		{
			_loggerInstance.LogException(LogLevel.Error, message, ex);
		}
	}
}
