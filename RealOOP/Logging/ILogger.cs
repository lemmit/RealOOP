using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealOOP.Logging
{
    public interface ILogger
    {
        void WriteLine(object @object);
        void Trace(object @object);
    }
}
