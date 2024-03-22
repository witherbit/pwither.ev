using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace pwither.ev.Exceptions
{
    public class DispatcherEventException<T> : ArgumentException
    {
        public object Instance { get; private set; }
        public MethodInfo MethodInfo { get; private set; }
        public Type[] Arguments { get => MethodInfo.GetGenericArguments(); }
        public T EventId { get; private set; }
        public DispatcherEventException(string message, object instance, MethodInfo info, T eventId) : base(message)
        {
            EventId = eventId;
            Instance = instance;
            MethodInfo = info;
        }
    }
}
