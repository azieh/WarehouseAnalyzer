using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;

namespace Common.Logging
{
    public class LoggerProvider : ILoggerProvider
    {
        public ILogger CreateLogger(string categoryName)
        {
            return new Logger();
        }

        public void Dispose()
        {
        }
    }
}
