namespace RealOOP
{
    public class Message
    {
        public virtual string MethodName => _methodName;
        public virtual object Payload => _payload;

        private readonly string _methodName;
        private readonly object _payload;
        public Message(string methodName, object payload = null)
        {
            _methodName = methodName;
            _payload = payload;
        }
    }

    public class Message<T> : Message
    {
        public T Payload => (T)base.Payload;
        public Message(string methodName, T payload = default(T)) 
            : base(methodName, payload)
        {
        }
    }
}
