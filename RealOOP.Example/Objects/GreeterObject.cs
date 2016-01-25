using System;
using System.Threading.Tasks;
using RealOOP.Example.Messages;
using RealOOP.Logging;

namespace RealOOP.Example.Objects
{
    public class GreeterObject : RealObject
    {
        public GreeterObject(ILogger logger = null) : base(logger)
        {
            AddMethod<GreetMessage>(new Method(async sender =>
                await Task.Run( () => {
                    Console.WriteLine("Tell me your name!");
                    var name = Console.ReadLine();
                    Console.WriteLine($"Hello {name}!");
                })));
        }
    }
}
