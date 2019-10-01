using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Attempt4.Helpers
{
    public interface ILogForHelper
    {
        void Warn(string message);
        void Error(string message, Exception ex);
    }
}