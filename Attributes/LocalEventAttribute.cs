using System;
using System.Collections.Generic;
using System.Text;

namespace pwither.ev
{
    [AttributeUsage(AttributeTargets.Method)]
    public class LocalEventAttribute : Attribute
    {
        public string Id { get; }

        public LocalEventAttribute(string id)
        {
            Id = id;
        }
    }
}
