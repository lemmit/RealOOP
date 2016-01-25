using System.Threading.Tasks;
using RealOOP.Logging;

namespace RealOOP.Objects
{
    public class DeadLetterObject : RealObject
    {
        public DeadLetterObject(ILogger logger = null) : base(logger)
        {
        }

        protected override Task Recv<T>(RealObject sender, T message)
        {
            return
                Task.Run(
                    () => {
                        Logger.Trace($"DeadLetter got a message! {message.GetType().Name}: {message.Payload ?? "[Empty Message]"}");
                        return true;
                    });
        }
    }
}
