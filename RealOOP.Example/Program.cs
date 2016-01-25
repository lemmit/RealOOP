using System;
using System.Threading.Tasks;
using RealOOP.Example.Messages;
using RealOOP.Example.Mixins;
using RealOOP.Example.Objects;
using RealOOP.Logging;
using RealOOP.Messages;
using RealOOP.Objects;

namespace RealOOP.Example
{
    class Program
    {
        private static async Task Examples()
        {
            var logger = new ConsoleLogger(ConsoleLogger.LoggingLevel.Off);
            var deadletter = new DeadLetterObject(logger);

            await CallOneObjectFromAnother(logger);
            await Inheritance(logger, deadletter);
            await InheritanceWithOverridenMethod(logger, deadletter);
            await ChangeMethodAtRuntime(logger, deadletter);
            await UseMixinsToCreateFinalObject(logger);
            FireAndForget(logger, new DeadLetterObject(new ConsoleLogger(ConsoleLogger.LoggingLevel.All)));
        }

        private static Task FireAndForget(ILogger logger, DeadLetterObject deadLetter)
        {
            Console.WriteLine("Sending async message to deadletter");
            var firstRealObject = new RealObject(logger);
            return firstRealObject.Send(deadLetter, new PingMessage());
        }

        static void Main()
        {
            Examples().Wait();
            Console.ReadLine();
        }

        private static async Task UseMixinsToCreateFinalObject(ILogger logger)
        {
            Console.WriteLine("Create object using mixins and call methods");
            var obj = new RealObject();
            obj.Mixin<SquareDrawerObject>();
            obj.Mixin<TreeDrawerObject>();
            var deadletter = new DeadLetterObject(logger);
            await deadletter.Send(obj, new DrawTreeMessage(3));
            await deadletter.Send(obj, new DrawSquareMessage(3));
        }

        private static async Task CallOneObjectFromAnother(ILogger logger)
        {
            var firstRealObject = new RealObject(logger);
            var secondRealObject = new RealObject(logger);
            Console.WriteLine($"Method calls between {firstRealObject.GetObjectRef()} and {secondRealObject.GetObjectRef()}");
            await firstRealObject.Send(secondRealObject, new PingMessage());
            Console.WriteLine();
        }

        private static async Task ChangeMethodAtRuntime(ILogger logger, DeadLetterObject deadletter)
        {
            Console.WriteLine("Define method at runtime using dynamic object");
            var dynamicObject = new DynamicObject(logger);
            Console.WriteLine("Define method at runtime [1st call - undefined]");
            await deadletter.Send(dynamicObject, new GreetMessage());
            Console.WriteLine("Define method at runtime [2nd call]");
            dynamicObject.AddOrUpdateMethod<GreetMessage>(new Method(async sender => await Task.Run( () => Console.WriteLine("Howdy!"))));
            await deadletter.Send(dynamicObject, new GreetMessage());
            Console.WriteLine();
        }

        private static async Task  InheritanceWithOverridenMethod(ILogger logger, DeadLetterObject deadletter)
        {
            Console.WriteLine("Method overloaded in derived type");
            var politeGreeter = new PoliteGreeterObject(logger);
            await deadletter.Send(politeGreeter, new GreetMessage());
            Console.WriteLine();
        }

        private static async Task Inheritance(ILogger logger, DeadLetterObject deadletter)
        {
            var derived = new GreeterObject(logger);
            Console.WriteLine("Method call message type");
            await deadletter.Send(derived, new GreetMessage());
            Console.WriteLine();
        }
    }
}
