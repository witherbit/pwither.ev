using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace pwither.ev
{
    internal class InstanceHandle
    {
        public object Instance { get; set; }
        public MethodInfo Method { get; set; }
    }
}
