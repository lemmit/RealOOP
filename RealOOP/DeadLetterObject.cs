using RealOOP.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealOOP
{
    public class DeadLetterObject : RealObject
    {
        public DeadLetterObject(ILogger logger = null) : base(logger)
        {
        }

        protected override void SendMessage(RealObject sender, Message message)
        {
            Logger.Trace($"DeadLetter got a message! {message.MethodName}: {message.Payload}");
        }
    }
}
