using System;
using System.Threading.Tasks;
using RealOOP.Example.Messages;
using RealOOP.Logging;

namespace RealOOP.Example.Objects
{
    public class PoliteGreeterObject : GreeterObject
    {
        public PoliteGreeterObject(ILogger logger = null) : base(logger)
        {
            AddMethod<GreetMessage>(new Method(async sender =>
                await Task.Run( ()=> {
                    Console.WriteLine("Could you tell me your name, please?");
                    var name = Console.ReadLine();
                    Console.WriteLine($"Welcome {name}!");
                })));
        }
    }
}
