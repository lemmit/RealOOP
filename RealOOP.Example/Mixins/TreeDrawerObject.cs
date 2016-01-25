using System;
using System.Linq;
using System.Threading.Tasks;
using RealOOP.Example.Messages;
using RealOOP.Logging;

namespace RealOOP.Example.Mixins
{
    public class TreeDrawerObject : RealObject
    {
        public TreeDrawerObject() : this(null) { }
        public TreeDrawerObject(ILogger logger) : base(logger)
        {
            AddMethod<DrawTreeMessage>(new Method<int>(async (sender, h) =>
                await Task.Run(() =>
                {

                    Enumerable.Range(0, h).ToList().ForEach(
                        h2 => Console.WriteLine("".PadLeft(h2 + 1, '*'))
                        );
                })));
        }
    }
}
