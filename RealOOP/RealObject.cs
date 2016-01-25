using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RealOOP.Defaultable;
using RealOOP.Logging;
using RealOOP.Messages;
using RealOOP.Objects;

namespace RealOOP
{
    public class RealObject
    {
        private readonly IDictionary<string, IMethod> _methods;
        private readonly object _methodsLock = new object();
        protected ILogger Logger;

        public RealObject() : this(new DummyLogger())
        {
        }

        public RealObject(ILogger logger)
        {
            Logger = logger;
            _methods = new DefaultableDictionary<string, IMethod>(
                new Dictionary<string, IMethod>(),
                new Method<object>(async (sender, payload) =>
                {
                    await Send(sender, new MethodNotFoundMessage(payload));
                }));

            AddMethod<MethodNotFoundMessage>(new Method<object>(async (sender, payload) =>
            {
                await Task.Run(() => Logger.Trace($"Method not found: {payload}"));
            }));

            AddMethod<PingMessage>(new Method(async sender =>
            {
                Logger.Trace($"Received Ping from {sender.GetObjectRef()}. Sending Pong.");
                await Send(sender, new PongMessage());
            }));

            AddMethod<PongMessage>(new Method(async sender =>
            {
                await Task.Run(() => Logger.Trace($"Received Pong from {sender.GetObjectRef()}"));
            }));
        }

        protected List<KeyValuePair<string, IMethod>> GetMethods()
        {
            List<KeyValuePair<string, IMethod>> methods;
            lock (_methodsLock)
            {
                methods = _methods.ToList();
            }
            return methods;
        }

        public string GetObjectRef()
        {
            return $"[{GetType().Name}|{GetHashCode()}]";
        }

        private void AddMethod(string messageTypeName, IMethod method)
        {
            lock (_methodsLock)
            {
                _methods[messageTypeName] = method;
            }
        }

        protected void AddMethod<T>(IMethod method)
            where T : Message
        {
            AddMethod(typeof (T).FullName, method);
        }

        public virtual async Task Send<T>(RealObject destinationObject, T message)
            where T : Message
        {
           await destinationObject.Recv(this, message);
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

        protected virtual async Task Recv<T>(RealObject sender, T message)
            where T : Message
        {
            Logger.Trace($"{GetObjectRef()} calls {message.GetType().Name} method in {sender.GetObjectRef()}");
            IMethod method;
            lock (_methodsLock)
            {
                method = _methods[message.GetType().FullName];
            }
            sender = sender ?? new DeadLetterObject();
            await method.Call(sender, message.Payload);
        }
    }
}
