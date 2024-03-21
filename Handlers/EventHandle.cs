using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace pwither.ev
{
    internal class EventHandle<T>
    {
        public T Id { get; set; }
        private List<InstanceHandle> _instances = new List<InstanceHandle>();
        public InstanceHandle[] Methods => _instances.ToArray();

        public void Add(InstanceHandle info)
        {
            _instances.Add(info);
        }

        public void Remove(InstanceHandle info)
        {
            _instances.Remove(info);
        }

        public void Clear()
        {
            _instances.Clear();
        }

        public bool Contains(InstanceHandle info)
        {
            return _instances.Contains(info);
        }

        public InstanceHandle GetInstanceHandle(object instance, MethodInfo info)
        {
            return _instances.FirstOrDefault(x => x.Instance == instance && x.Method == info);
        }
    }
}
