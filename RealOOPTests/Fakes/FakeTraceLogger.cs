﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealOOP.Defaultable;
using RealOOP.Logging;

namespace RealOOPTests.Fakes
{
    internal class FakeTraceLogger : ILogger
    {
        public int NumberOfCalls
        {
            get
            {
                lock (sync)
                {
                    return _call;
                }
            }
        }
        private readonly IList<Func<object, bool>> _calls 
            = new DefaultableList<Func<object, bool>>(o => false);
        private int _call;
        private object sync = new object();
        public FakeTraceLogger FirstCall(Func<object, bool> call)
        {
            return AndThenCall(call);
        }
        public FakeTraceLogger AndThenCall(Func<object, bool> call)
        {
            _calls.Add(call);
            return this;
        }

        public FakeTraceLogger IgnoreCall()
        {
            _calls.Add(o => true);
            return this;
        }

        public void WriteLine(object @object)
        {
            lock (sync)
            {
                
            }
        }

        public void Trace(object @object)
        {
            lock (sync)
            {
                if (!_calls[_call](@object))
                {
                    throw new Exception($"[Call {_call}] {@object.ToString()}");
                }
                _call++;
            }
        }
    }

}
