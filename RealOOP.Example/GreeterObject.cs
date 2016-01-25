using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealOOP.Logging;

namespace RealOOP.Example
{
    public class GreeterObject : RealObject
    {
        public GreeterObject(ILogger logger = null) : base(logger)
        {
            AddMethod(new Method("Greet", sender =>
            {
                Console.WriteLine("Tell me your name!");
                var name = Console.ReadLine();
                Console.WriteLine($"Hello {name}!");
            } ));
        }
    }
}
