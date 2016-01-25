using RealOOP.Logging;

namespace RealOOP.Objects
{
    public class DynamicObject : RealObject
    {
        public void AddOrUpdateMethod<T>(IMethod method)
            where T : Message
        {
            AddMethod<T>(method);
        }

        public DynamicObject(ILogger logger = null) : base(logger)
        {
        }
    }
}
