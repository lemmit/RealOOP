using System;
using System.Linq;
using RealOOP.Logging;

namespace RealOOP.Example.Mixins
{
    public class TreeDrawerObject : RealObject
    {
        public TreeDrawerObject() : this(null) { }
        public TreeDrawerObject(ILogger logger) : base(logger)
        {
            AddMethod(new Method<int>("DrawTree", (sender, h) =>
            {
                Enumerable.Range(0,h).ToList().ForEach(
                    h2 => Console.WriteLine("".PadLeft(h2+1,'*'))
                    );
            }));
        }
    }
}
