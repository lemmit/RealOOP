using RealOOP.Logging;

namespace RealOOP.Objects
{
    public class DeadLetterObject : RealObject
    {
        public DeadLetterObject(ILogger logger = null) : base(logger)
        {
        }

        protected override void Recv<T>(RealObject sender, T message)
        {
            Logger.Trace($"DeadLetter got a message! {message.GetType().Name}: {message.Payload}");
        }
    }
}
