using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zenith.Client.Shared.Util
{
    public static class Reflection
    {
        public static Type WhichImplements<T>(this Type[] types) where T : class
        {
            foreach(Type t in types)
            {
                if (typeof(T).IsAssignableFrom(t))
                    return t;
            }

            return null;
        }
    }
}
