using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealOOP.Example.Messages
{
    public class DrawTreeMessage : Message<int>
    {
        public DrawTreeMessage(int height) : base(height)
        {
        }
    }
}
