using System.Collections.Generic;
using System.Linq;
using RealOOP.Defaultable;
using RealOOP.Logging;
using RealOOP.Messages;
using RealOOP.Objects;

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
                new Method<object>((sender, payload) =>
                {
                    Send(sender, new MethodNotFoundMessage(payload));
                }));

            AddMethod<MethodNotFoundMessage>(new Method<object>((sender, payload) =>
            {
                Logger.Trace($"Method not found: {payload}");
            }));

            AddMethod<PingMessage>(new Method(sender =>
            {
                Logger.Trace($"Received Ping from {sender.GetObjectRef()}. Sending Pong.");
                Send(sender, new PongMessage());
            }));

            AddMethod<PongMessage>(new Method(sender =>
            {
                Logger.Trace($"Received Pong from {sender.GetObjectRef()}");
            }));
        }

        protected List<KeyValuePair<string, IMethod>> GetMethods()
        {
            return _methods.ToList();
        }

        public string GetObjectRef()
        {
            return $"[{GetType().Name}|{GetHashCode()}]";
        }

        private void AddMethod(string messageTypeName, IMethod method)
        {
            _methods[messageTypeName] = method;
        }

        protected void AddMethod<T>(IMethod method)
            where T : Message
        {
            AddMethod(typeof (T).FullName, method);
        }

        public virtual void Send<T>(RealObject destinationObject, T message)
            where T : Message
        {
            destinationObject.Recv(this, message);
        }

        public void Mixin<T>()
           where T : RealObject, new()
        {
            var mixin = new T();
            mixin.Logger = Logger;
            mixin.GetMethods()
                .ForEach(namedMethod => 
                    AddMethod(namedMethod.Key, namedMethod.Value)
                );
        }

        protected virtual void Recv<T>(RealObject sender, T message)
            where T : Message
        {
            Logger.Trace($"{GetObjectRef()} calls {message.GetType().Name} method in {sender.GetObjectRef()}");
            var method = _methods[message.GetType().FullName];
            sender = sender ?? new DeadLetterObject();
            method.Call(sender, message.Payload);
        }
    }
}
