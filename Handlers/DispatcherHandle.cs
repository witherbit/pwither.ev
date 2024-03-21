using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Reflection;
using System.Text;

namespace pwither.ev
{
    internal class DispatcherHandle
    {
        private List<EventHandle<string>> eventHandlers = new List<EventHandle<string>>();

        public void Register(object handler)
        {
            Type type = handler.GetType();
            MethodInfo[] methods = type.GetMethods();

            foreach (var method in methods)
            {
                var attribute = (LocalEventAttribute)method.GetCustomAttribute(typeof(LocalEventAttribute), false);
                if (attribute != null)
                {
                    var evHandle = eventHandlers.FirstOrDefault(x => x.Id == attribute.Id);
                    if(evHandle == null)
                    {
                        evHandle = new EventHandle<string>
                        {
                            Id = attribute.Id,
                        };
                        if (evHandle.GetInstanceHandle(handler, method) == null)
                            evHandle.Add(new InstanceHandle
                            {
                                Instance = handler,
                                Method = method,
                            });
                        eventHandlers.Add(evHandle);
                    }
                    else
                    {
                        if (evHandle.GetInstanceHandle(handler, method) == null)
                            evHandle.Add(new InstanceHandle
                            {
                                Instance = handler,
                                Method = method,
                            });
                    }
                }
            }
        }
        public void Unregister(object handler)
        {
            Type type = handler.GetType();
            MethodInfo[] methods = type.GetMethods();

            foreach (var method in methods)
            {
                var attribute = (LocalEventAttribute)method.GetCustomAttribute(typeof(LocalEventAttribute), false);
                if (attribute != null)
                {
                    var evHandle = eventHandlers.FirstOrDefault(x => x.Id == attribute.Id);
                    var instanceHandle = evHandle.GetInstanceHandle(handler, method);
                    if (instanceHandle != null)
                        evHandle.Remove(instanceHandle);
                }
            }
        }
        public void UnregisterAll()
        {
            foreach (var handle in eventHandlers)
                handle.Clear();
        }
        public void RegisterByEventId(object handler, string id)
        {
            Type type = handler.GetType();
            MethodInfo[] methods = type.GetMethods();

            foreach (var method in methods)
            {
                var attribute = (LocalEventAttribute)method.GetCustomAttribute(typeof(LocalEventAttribute), false);
                if (attribute != null && attribute.Id == id)
                {
                    var evHandle = eventHandlers.FirstOrDefault(x => x.Id == attribute.Id);
                    if (evHandle == null)
                    {
                        evHandle = new EventHandle<string>
                        {
                            Id = attribute.Id,
                        };
                        if (evHandle.GetInstanceHandle(handler, method) == null)
                            evHandle.Add(new InstanceHandle
                            {
                                Instance = handler,
                                Method = method,
                            });
                        eventHandlers.Add(evHandle);
                    }
                    else
                    {
                        if (evHandle.GetInstanceHandle(handler, method) == null)
                            evHandle.Add(new InstanceHandle
                            {
                                Instance = handler,
                                Method = method,
                            });
                    }
                }
            }
        }
        public void UnregisterByEventId(object handler, string id)
        {
            Type type = handler.GetType();
            MethodInfo[] methods = type.GetMethods();

            foreach (var method in methods)
            {
                var attribute = (LocalEventAttribute)method.GetCustomAttribute(typeof(LocalEventAttribute), false);
                if (attribute != null && attribute.Id == id)
                {
                    var evHandle = eventHandlers.FirstOrDefault(x => x.Id == attribute.Id);
                    var instanceHandle = evHandle.GetInstanceHandle(handler, method);
                    if (instanceHandle != null)
                        evHandle.Remove(instanceHandle);
                }
            }
        }
        public void UnregisterAllByEventId(string id)
        {
            foreach (var handle in eventHandlers)
            {
                if(handle.Id == id)
                handle.Clear();
            }
        }
        public void Invoke(string id, params object[] eventArgs)
        {
            var evHandle = eventHandlers.FirstOrDefault(x => x.Id == id);
            if (evHandle != null)
            {
                foreach(var intanceHandle in evHandle.Methods)
                {
                    if (intanceHandle.Method.GetParameters().Length == eventArgs.Length)
                    {
                        object handlerInstance = Activator.CreateInstance(intanceHandle.Method.DeclaringType);
                        intanceHandle.Method?.Invoke(handlerInstance, eventArgs);
                    }
                    else
                    {
                        throw new ArgumentException($"The arguments for the called method '{id}' are incorrect or missing");
                    }
                }
            }
            else
            {
                throw new Exception($"Handler for the event {id} not found");
            }
        }
    }
}
