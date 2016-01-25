using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RealOOP;
using RealOOP.Messages;
using RealOOPTests.Fakes;

namespace RealOOPTests
{
    [TestClass]
    public class RealObjectTests
    {
       
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task PingObjectAndRecievePong_missspell1()
        {
            var logger = new FakeTraceLogger();
            logger.FirstCall(str => str.ToString().Contains("call Ping"));
            var firstRealObject = new RealObject(logger);
            var secondRealObject = new RealObject(logger);
            await firstRealObject.Send(secondRealObject, new PingMessage());
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task PingObjectAndRecievePong_missspell2()
        {
            var logger = new FakeTraceLogger();
            logger.IgnoreCall()
                .AndThenCall(str => str.ToString().Contains("Recieved Ping"));
                
            var firstRealObject = new RealObject(logger);
            var secondRealObject = new RealObject(logger);
            await firstRealObject.Send(secondRealObject, new PingMessage());
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task PingObjectAndRecievePong_missspell3()
        {
            var logger = new FakeTraceLogger();
            logger.IgnoreCall()
                .IgnoreCall()
                .AndThenCall(str => str.ToString().Contains("call Pong"));
            var firstRealObject = new RealObject(logger);
            var secondRealObject = new RealObject(logger);
            await firstRealObject.Send(secondRealObject, new PingMessage());
        }


        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public async Task PingObjectAndRecievePong_missspell4()
        {
            var logger = new FakeTraceLogger();
            logger.IgnoreCall()
                .IgnoreCall()
                .IgnoreCall()
                .AndThenCall(str => str.ToString().Contains("Recieved Pong"));
            var firstRealObject = new RealObject(logger);
            var secondRealObject = new RealObject(logger);
            await firstRealObject.Send(secondRealObject, new PingMessage());
        }

        [TestMethod]
        public async Task PingObjectAndRecievePong()
        {
            var logger = new FakeTraceLogger();
            logger
                .FirstCall(str => str.ToString().Contains("calls Ping"))
                .AndThenCall(str => str.ToString().Contains("Received Ping"))
                .AndThenCall(str => str.ToString().Contains("calls Pong"))
                .AndThenCall(str => str.ToString().Contains("Received Pong"));
            var firstRealObject = new RealObject(logger);
            var secondRealObject = new RealObject(logger);
            await firstRealObject.Send(secondRealObject, new PingMessage());
            Assert.AreEqual(4, logger.NumberOfCalls);
        }
    }
}
