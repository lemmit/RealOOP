using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealOOP.Logging
{
    public class ConsoleLogger : ILogger
    {
        [Flags]
        public enum LoggingLevel : uint
        {
            Off = 0,
            Trace = 1,
            Debug = 2,
            All = Trace | Debug
        }
        private readonly LoggingLevel _loggingLevel;
        public ConsoleLogger(LoggingLevel loggingLevel = LoggingLevel.All)
        {
            _loggingLevel = loggingLevel;
        }

        public void Trace(object @object)
        {
            if((_loggingLevel & LoggingLevel.Trace) != 0)
                Console.WriteLine(@object);
        }

        public void WriteLine(object @object)
        {
            if ((_loggingLevel & LoggingLevel.All) != 0)
                Console.WriteLine(@object);
        }
    }
}
