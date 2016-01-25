using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        static void Main(string[] args)
        {
            var logger = new ConsoleLogger(ConsoleLogger.LoggingLevel.Off);
            var deadletter = new DeadLetterObject(logger);

            CallOneObjectFromAnother(logger);
            Console.WriteLine();

            Inheritance(logger, deadletter);
            Console.WriteLine();

            InheritanceWithOverridenMethod(logger, deadletter);
            Console.WriteLine();

            ChangeMethodAtRuntime(logger, deadletter);
            Console.WriteLine();

            UseMixinsToCreateFinalObject(logger);
            Console.WriteLine();


            Console.ReadLine();
        }

        private static void UseMixinsToCreateFinalObject(ILogger logger)
        {
            Console.WriteLine("Create object using mixins and call methods");
            var obj = new RealObject();
            obj.Mixin<SquareDrawerObject>();
            obj.Mixin<TreeDrawerObject>();
            var deadletter = new DeadLetterObject(logger);
            deadletter.Send(obj, new DrawTreeMessage(3));
            deadletter.Send(obj, new DrawSquareMessage(3));
        }

        private static void CallOneObjectFromAnother(ILogger logger)
        {
            var firstRealObject = new RealObject(logger);
            var secondRealObject = new RealObject(logger);
            Console.WriteLine($"Method calls between {firstRealObject.GetObjectRef()} and {secondRealObject.GetObjectRef()}");
            firstRealObject.Send(secondRealObject, new PingMessage());
        }

        private static void ChangeMethodAtRuntime(ILogger logger, DeadLetterObject deadletter)
        {
            Console.WriteLine("Define method at runtime using dynamic object");
            var dynamicObject = new DynamicObject(logger);
            Console.WriteLine("Define method at runtime [1st call - undefined]");
            deadletter.Send(dynamicObject, new GreetMessage());
            Console.WriteLine("Define method at runtime [2nd call]");
            dynamicObject.AddOrUpdateMethod<GreetMessage>(new Method(sender => { Console.WriteLine("Howdy!"); }));
            deadletter.Send(dynamicObject, new GreetMessage());
        }

        private static void InheritanceWithOverridenMethod(ILogger logger, DeadLetterObject deadletter)
        {
            Console.WriteLine("Method overloaded in derived type");
            var politeGreeter = new PoliteGreeterObject(logger);
            deadletter.Send(politeGreeter, new GreetMessage());
        }

        private static void Inheritance(ILogger logger, DeadLetterObject deadletter)
        {
            var derived = new GreeterObject(logger);
            Console.WriteLine("Method call message type");
            deadletter.Send(derived, new GreetMessage());
        }
    }
}
