using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pwither.ev
{
    public class GenericEventDispatcher<T>
    {
        private static GenericDispatcherHandle<T> dispatcher = new GenericDispatcherHandle<T>();

        public static void Register(object handler)
        {
            dispatcher.Register(handler);
        }

        public static void Unregister(object handler)
        {
            dispatcher.Unregister(handler);
        }
        public static void UnregisterAll()
        {
            dispatcher.UnregisterAll();
        }
        public static void RegisterByEventId(object handler, T id)
        {
            dispatcher.RegisterByEventId(handler, id);
        }
        public static void UnregisterByEventId(object handler, T id)
        {
            dispatcher.UnregisterByEventId(handler, id);
        }
        public static void UnregisterAllByEventId(T id)
        {
            dispatcher.UnregisterAllByEventId(id);
        }

        public static void Invoke(T id, params object[] eventArgs)
        {
            dispatcher.Invoke(id, eventArgs);
        }

        public static async Task InvokeAsync(T id, params object[] eventArgs)
        {
            await dispatcher.InvokeAsync(id, eventArgs);
        }
    }
}
