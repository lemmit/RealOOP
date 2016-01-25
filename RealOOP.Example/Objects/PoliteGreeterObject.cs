using System;
using RealOOP.Example.Messages;
using RealOOP.Logging;

namespace RealOOP.Example.Objects
{
    public class PoliteGreeterObject : GreeterObject
    {
        public PoliteGreeterObject(ILogger logger = null) : base(logger)
        {
            AddMethod<GreetMessage>(new Method(sender =>
            {
                Console.WriteLine("Could you tell me your name, please?");
                var name = Console.ReadLine();
                Console.WriteLine($"Welcome {name}!");
            }));
        }
    }
}
