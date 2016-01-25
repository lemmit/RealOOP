using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RealOOP;
using RealOOP.Logging;
using RealOOPTests.Fakes;

namespace RealOOPTests
{
    [TestClass]
    public class InheritanceTests
    {
        private class Base : RealObject
        {
            public Base(ILogger logger = null) : base(logger) 
            {
                AddMethod(new Method("TestMethod", sender => 
                    SendMessageTo(
                        sender, 
                        new Message<string>("TestMethodResponse", "Hi from test method [base]")
                        )
                    ));
                AddMethod(new Method("TestMethodResponse", sender =>
                    Logger.Trace("Hi from test method response [base]"))
                );
            }
        }

        private class Derived : Base
        {
            public Derived(ILogger logger = null) : base(logger) { }
        }

        private class DerivedWithOverridenMethod : Base
        {
            public DerivedWithOverridenMethod(ILogger logger = null)
                : base(logger)
            {
                //redefine
                AddMethod(new Method("TestMethodResponse", sender =>
                    Logger.Trace("Hi from test method response [derived]"))
                );
            }
        }

        [TestMethod]
        public void DeriveMethodOfTheBaseClass()
        {
            var logger = new FakeTraceLogger()
                    .FirstCall(o => o.ToString().Contains("calls TestMethod"))
                    .AndThenCall(o => o.ToString().Contains("calls TestMethodResponse"))
                    .AndThenCall(o => o.ToString().Contains("test method response [base]"))
                    ;
            var baseObj = new Base(logger);
            var derivedObj = new Derived(logger);
            baseObj.SendMessageTo(derivedObj, new Message("TestMethod"));
            Assert.AreEqual(3, logger.NumberOfCalls);
        }

        [TestMethod]
        public void RedefineMethodInTheDerivedClass()
        {
            var logger = new FakeTraceLogger()
                    .FirstCall(o => o.ToString().Contains("calls TestMethod"))
                    .AndThenCall(o => o.ToString().Contains("calls TestMethodResponse"))
                    .AndThenCall(o => o.ToString().Contains("test method response [derived]"))
                    ;
            var baseObj = new Base(logger);
            var derivedObj = new DerivedWithOverridenMethod(logger);
            derivedObj.SendMessageTo(baseObj, new Message("TestMethod"));
            Assert.AreEqual(3, logger.NumberOfCalls);
        }

        [TestMethod]
        public void RedefineMethodInTheTheDerivedClassButBaseStaysUnmodified()
        {
            var logger = new FakeTraceLogger()
                    .FirstCall(o => o.ToString().Contains("calls TestMethod"))
                    .AndThenCall(o => o.ToString().Contains("calls TestMethodResponse"))
                    .AndThenCall(o => o.ToString().Contains("test method response [base]"))
                    ;
            var baseObj = new Base(logger);
            var derivedObj = new DerivedWithOverridenMethod(logger);
            baseObj.SendMessageTo(derivedObj, new Message("TestMethod"));
            Assert.AreEqual(3, logger.NumberOfCalls);
        }
    }
}
