using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace pwither.ev
{
    public class GenericEventScript<T>
    {
        public void InvokeEvent(T id, params object[] eventArgs)
        {
            Type type = this.GetType();
            MethodInfo[] methods = type.GetMethods();

            foreach (var method in methods)
            {
                var attribute = (GenericEventAttribute<T>)method.GetCustomAttribute(typeof(GenericEventAttribute<T>), false);
                if (attribute != null && id.CompareGenerics(attribute.Id))
                {
                    if (method.GetParameters().Length == eventArgs.Length)
                    {
                        method?.Invoke(this, eventArgs);
                    }
                    else
                    {
                        throw new ArgumentException($"The arguments for the called method '{id.ToString()}' are incorrect or missing");
                    }
                }
            }
        }

        public async Task InvokeEventAsync(T id, params object[] eventArgs)
        {
            Type type = this.GetType();
            MethodInfo[] methods = type.GetMethods();

            foreach (var method in methods)
            {
                var attribute = (GenericEventAttribute<T>)method.GetCustomAttribute(typeof(GenericEventAttribute<T>), false);
                if (attribute != null && id.CompareGenerics(attribute.Id))
                {
                    if (method.GetParameters().Length == eventArgs.Length)
                    {
                        await Task.Run(() =>
                        {
                            method?.Invoke(this, eventArgs);
                        });
                    }
                    else
                    {
                        throw new ArgumentException($"The arguments for the called method '{id.ToString()}' are incorrect or missing");
                    }
                }
            }
        }
    }
}
