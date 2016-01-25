using System;

namespace RealOOP
{
    public interface IMethod
    {
        void Call(RealObject sender, object message = null);
        string Name { get; }
    }

    public class Method : IMethod
    {
        private readonly Action<RealObject> _action;
        private readonly string _name;

        public Method(string name, Action<RealObject> methodAction)
        {
            if (methodAction == null || name == null)
                throw new ArgumentNullException();
            _action = methodAction;
            _name = name;
        }

        public void Call(RealObject sender, object message = null)
        {
            _action(sender);
        }

        public string Name => _name;
    }

    public class Method<T> : IMethod
    {
        private readonly Action<RealObject, T> _action;
        private readonly string _name;

        public Method(string name, Action<RealObject, T> methodAction)
        {
            if(methodAction == null || name == null)
                throw new ArgumentNullException();
            _action = methodAction;
            _name = name;
        }
        
        public void Call(RealObject sender, object message = null)
        {
            var t = typeof (T);
            _action(sender, (T)message);
        }

        public string Name => _name;
        public Type ParameterType => typeof (T);
    }
}