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
        public object Instance { get; }
        public MethodInfo MethodInfo { get; }
        public MethodParam[] Parameters { get; }
        public string ParametersString { get; }
        public T EventId { get;}
        public DispatcherEventException(string message, object instance, MethodInfo info, T eventId) : base(message)
        {
            ParametersString = "";
            EventId = eventId;
            Instance = instance;
            MethodInfo = info;
            var parameters = new List<MethodParam>();
            var mPars = MethodInfo.GetParameters();
            foreach (var param in mPars)
            {
                parameters.Add(new MethodParam
                {
                    Type = param.ParameterType,
                    Name = param.Name
                });
                if(param != mPars.Last())
                    ParametersString += $"{param.ParameterType} {param.Name}, ";
                else
                    ParametersString += $"{param.ParameterType} {param.Name}";
            }
        }
    }
}
