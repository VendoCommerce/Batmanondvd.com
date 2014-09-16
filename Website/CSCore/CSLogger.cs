using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSCore.Logging;

namespace CSCore
{
	public class CSLogger : ILogger
	{
		static ILogger _logger;
		static CSLogger()
		{
			_logger = new NLogService();
		}

		public static ILogger Instance
		{
			get
			{
				return _logger;
			}
		}

		public void LogException(string message, Exception ex)
		{
			Instance.LogException(message, ex);
		}
	}
}



