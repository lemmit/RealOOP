using Microsoft.VisualStudio.TestTools.UnitTesting;
using RealOOP;
using RealOOP.Logging;
using RealOOPTests.Fakes;

namespace RealOOPTests
{
    [TestClass]
    public class InheritanceTests
    {
        private class TestMessage : Message<string>
        {
            public TestMessage(string msg) : base(msg) { }
        }
        private class TestMessageResponse : Message<string>
        {
            public TestMessageResponse(string msg) : base(msg) { }
        }

        private class Base : RealObject
        {
            public Base(ILogger logger = null) : base(logger) 
            {
                AddMethod<TestMessage>(new Method(sender => 
                    Send(
                        sender, 
                        new TestMessageResponse("Hi from test method [base]")
                        )
                    ));
                AddMethod<TestMessageResponse>(new Method(sender =>
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
                AddMethod<TestMessageResponse>(new Method<string>( (sender, str) =>
                    Logger.Trace("Hi from test method response [derived]")
                ));
            }
        }

        [TestMethod]
        public void DeriveMethodOfTheBaseClass()
        {
            var logger = new FakeTraceLogger()
                    .FirstCall(o => o.ToString().Contains("calls TestMessage"))
                    .AndThenCall(o => o.ToString().Contains("calls TestMessageResponse"))
                    .AndThenCall(o => o.ToString().Contains("test method response [base]"))
                    ;
            var baseObj = new Base(logger);
            var derivedObj = new Derived(logger);
            baseObj.Send(derivedObj, new TestMessage("TestMethod"));
            Assert.AreEqual(3, logger.NumberOfCalls);
        }

        [TestMethod]
        public void RedefineMethodInTheDerivedClass()
        {
            var logger = new FakeTraceLogger()
                    .FirstCall(o => o.ToString().Contains("calls TestMessage"))
                    .AndThenCall(o => o.ToString().Contains("calls TestMessageResponse"))
                    .AndThenCall(o => o.ToString().Contains("test method response [derived]"))
                    ;
            var baseObj = new Base(logger);
            var derivedObj = new DerivedWithOverridenMethod(logger);
            derivedObj.Send(baseObj, new TestMessage("TestMethod"));
            Assert.AreEqual(3, logger.NumberOfCalls);
        }

        [TestMethod]
        public void RedefineMethodInTheTheDerivedClassButBaseStaysUnmodified()
        {
            var logger = new FakeTraceLogger()
                    .FirstCall(o => o.ToString().Contains("calls TestMessage"))
                    .AndThenCall(o => o.ToString().Contains("calls TestMessageResponse"))
                    .AndThenCall(o => o.ToString().Contains("test method response [base]"))
                    ;
            var baseObj = new Base(logger);
            var derivedObj = new DerivedWithOverridenMethod(logger);
            baseObj.Send(derivedObj, new TestMessage("TestMethod"));
            Assert.AreEqual(3, logger.NumberOfCalls);
        }
    }
}
