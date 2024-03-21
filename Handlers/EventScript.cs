using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Reflection;
using System.Text;

namespace pwither.ev
{
    public class EventScript
    {
        public void InvokeEvent(string id, params object[] eventArgs)
        {
            Type type = this.GetType();
            MethodInfo[] methods = type.GetMethods();

            foreach (var method in methods)
            {
                var attribute = (LocalEventAttribute)method.GetCustomAttribute(typeof(LocalEventAttribute), false);
                if (attribute != null && attribute.Id == id)
                {
                    if (method.GetParameters().Length == eventArgs.Length)
                    {
                        method?.Invoke(this, eventArgs);
                    }
                    else
                    {
                        throw new ArgumentException($"The arguments for the called method '{id}' are incorrect or missing");
                    }
                }
            }
        }
    }
}
