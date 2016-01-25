namespace RealOOP
{
    public abstract class Message
    {
        public virtual object Payload => _payload;
        private readonly object _payload;
        protected Message(object payload = null)
        {
            _payload = payload;
        }
    }

    public abstract class Message<T> : Message
    {
        public virtual T Payload => (T)base.Payload;
        protected Message(T payload = default(T)) : base(payload)
        {
        }
    }

}
