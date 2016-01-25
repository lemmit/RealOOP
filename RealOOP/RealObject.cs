using System.Collections.Generic;
using System.Linq;
using RealOOP.Defaultable;
using RealOOP.Logging;

namespace RealOOP
{
    public class RealObject
    {
        private readonly IDictionary<string, IMethod> _methods;

        protected ILogger Logger;

        public RealObject() : this(new DummyLogger())
        {
        }

        public RealObject(ILogger logger)
        {
            Logger = logger;
            _methods = new DefaultableDictionary<string, IMethod>(
                new Dictionary<string, IMethod>(),
                new Method<object>("IfMethodNotFound", (sender, payload) =>
                {
                    SendMessageTo(sender, new Message<object>("MethodNotFound", payload));
                }));

            AddMethod(new Method<object>("MethodNotFound", (sender, payload) =>
            {
                Logger.Trace($"Method not found: {payload}");
            }));
            AddMethod(new Method("Ping", sender =>
            {
                Logger.Trace($"Received Ping from {sender.GetObjectRef()}. Sending Pong.");
                SendMessageTo(sender, new Message("Pong"));
            }));
            AddMethod(new Method("Pong", sender =>
            {
                Logger.Trace($"Received Pong from {sender.GetObjectRef()}");
            }));
        }

        protected IEnumerable<IMethod> GetMethods()
        {
            return _methods.Values;
        }

        public string GetObjectRef()
        {
            return $"[{GetType().Name}|{GetHashCode()}]";
        }

        protected void AddMethod(IMethod method)
        {
            _methods[method.Name] = method;
        }

        public virtual void SendMessageTo(RealObject destinationObject, Message message)
        {
            destinationObject.SendMessage(this, message);
        }

        public void Mixin<M>()
           where M : RealObject, new()
        {
            var mixin = new M();
            mixin.Logger = Logger;
            mixin.GetMethods().ToList().ForEach(method => AddMethod(method));
        }

        protected virtual void SendMessage(RealObject sender, Message message)
        {
            Logger.Trace($"{GetObjectRef()} calls {message.MethodName} method in {sender.GetObjectRef()}");
            var method = _methods[message.MethodName];
            sender = sender ?? new DeadLetterObject();
            method.Call(sender, message.Payload);
        }
    }
}
