﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealOOP.Logging;

namespace RealOOP.Example.Mixins
{
    public class SquareDrawerObject : RealObject
    {
        public SquareDrawerObject() : this(null)
        {
        }
        public SquareDrawerObject(ILogger logger) : base(logger)
        {
            AddMethod(new Method<int>("DrawSquare", (sender, h) =>
            {
                Enumerable.Range(0, h).ToList().ForEach(
                    _ => Console.WriteLine("".PadLeft(h, '#'))
                    );
            }));
        }
    }
}