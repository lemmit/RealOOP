using System;

namespace RealOOP
{
    public interface IMethod
    {
        void Call(RealObject sender, object message = null);
    }

    public class Method : IMethod
    {
        private readonly Action<RealObject> _action;

        public Method(Action<RealObject> methodAction)
        {
            if (methodAction == null)
                throw new ArgumentNullException();
            _action = methodAction;
        }

        public void Call(RealObject sender, object message = null)
        {
            _action(sender);
        }
    }

    public class Method<T> : IMethod
    {
        private readonly Action<RealObject, T> _action;

        public Method(Action<RealObject, T> methodAction)
        {
            if(methodAction == null)
                throw new ArgumentNullException();
            _action = methodAction;
        }
        
        public void Call(RealObject sender, object message = null)
        {
            var t = typeof (T);
            _action(sender, (T)message);
        }

        public Type ParameterType => typeof (T);
    }
}