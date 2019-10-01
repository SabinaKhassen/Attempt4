using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Attempt4.Helpers
{
    public class Log4NetHelper : ILogForHelper
    {
        private readonly log4net.ILog _log = log4net.LogManager.GetLogger(Assembly.GetExecutingAssembly().FullName);

        public void Error(string message, Exception ex)
        {
            _log.Error(message, ex);
        }

        public void Warn(string message)
        {
            _log.Warn(message);
        }
    }
}