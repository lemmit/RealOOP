using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealOOP.Logging
{
    public class DummyLogger : ILogger
    {
        public void Trace(object @object)
        {
        }

        public void WriteLine(object @object)
        {
        }
    }
}
