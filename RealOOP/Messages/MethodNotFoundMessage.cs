using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealOOP.Messages
{
    public class MethodNotFoundMessage : Message
    {
        public MethodNotFoundMessage(object payload) : base(payload)
        {
        }
    }
}
