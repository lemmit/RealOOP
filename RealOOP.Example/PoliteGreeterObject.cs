using RealOOP.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealOOP.Example
{
    public class PoliteGreeterObject : GreeterObject
    {
        public PoliteGreeterObject(ILogger logger = null) : base(logger)
        {
            AddMethod(new Method("Greet", sender =>
            {
                Console.WriteLine("Could you tell me your name, please?");
                var name = Console.ReadLine();
                Console.WriteLine($"Welcome {name}!");
            }));
        }
    }
}
