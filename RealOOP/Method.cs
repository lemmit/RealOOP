using System;
using System.Threading.Tasks;

namespace RealOOP
{
    public interface IMethod
    {
        Task Call(RealObject sender, object message = null);
    }

    public class Method : IMethod
    {
        private readonly Func<RealObject, Task> _action;

        public Method(Func<RealObject, Task> methodAction)
        {
            if (methodAction == null)
                throw new ArgumentNullException();
            _action = methodAction;
        }

        public Method(Action<RealObject> methodAction)
        {
            if (methodAction == null)
                throw new ArgumentNullException();
            _action = async (sender) => await Task.Run( () => methodAction(sender));
        }

        public Task Call(RealObject sender, object message = null)
        {
            return _action(sender);
        }
    }

    public class Method<T> : IMethod
    {
        private readonly Func<RealObject, T, Task> _action;

        public Method(Func<RealObject, T, Task> methodAction)
        {
            if(methodAction == null)
                throw new ArgumentNullException();
            _action = methodAction;
        }

        public Method(Action<RealObject, T> methodAction)
        {
            if (methodAction == null)
                throw new ArgumentNullException();
            _action = async (sender, msg) => await Task.Run(() => methodAction(sender, msg));
        }

        public Task Call(RealObject sender, object message = null)
        {
            var t = typeof (T);
            return _action(sender, (T)message);
        }

        public Type ParameterType => typeof (T);
    }
}