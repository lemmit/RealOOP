using RealOOP.Logging;

namespace RealOOP.Example
{
    public class DynamicObject : RealObject
    {
        public void AddOrUpdateMethod(IMethod method)
        {
            AddMethod(method);
        }

        public DynamicObject(ILogger logger = null) : base(logger)
        {
        }
    }
}
