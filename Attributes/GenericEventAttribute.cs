using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pwither.ev
{
    [AttributeUsage(AttributeTargets.Method)]
    public class GenericEventAttribute<T> : Attribute
    {
        public T Id { get; }

        public GenericEventAttribute(T id)
        {
            Id = id;
        }
    }
}
