using System;
using System.Collections.Generic;
using System.Text;

namespace pwither.ev
{
    public static class EventDispatcher
    {
        private static DispatcherHandle dispatcher = new DispatcherHandle();

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
        public static void RegisterByEventId(object handler, string id)
        {
            dispatcher.RegisterByEventId(handler, id);
        }
        public static void UnregisterByEventId(object handler, string id)
        {
            dispatcher.UnregisterByEventId(handler, id);
        }
        public static void UnregisterAllByEventId(string id)
        {
            dispatcher.UnregisterAllByEventId(id);
        }

        public static void Invoke(string id, params object[] eventArgs)
        {
            dispatcher.Invoke(id, eventArgs);
        }
    }
}
