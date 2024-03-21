using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pwither.ev
{
    internal static class Extensions
    {
        public static bool CompareGenerics<T>(this T first, T second)
        {
            return EqualityComparer<T>.Default.Equals(first, second);
        }
    }
}
