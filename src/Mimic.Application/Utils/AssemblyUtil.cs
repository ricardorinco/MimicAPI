using System;
using System.Linq;
using System.Reflection;

namespace Mimic.Application.Utils
{
    public static class AssemblyUtil
    {
        public static Type[] GetTypes<Handler>(string nameSpace)
        {
            return GetTypesInNamespace(
                Assembly.GetExecutingAssembly(),
                $"{typeof(Handler).Namespace}.{nameSpace}"
            );
        }

        public static Type[] GetTypesInNamespace(Assembly assembly, string nameSpace)
        {
            return assembly.GetTypes()
                .Where(t =>
                    string.Equals(t.Namespace, nameSpace, StringComparison.Ordinal)
                    && t.IsPublic
                )
                .ToArray();
        }

        
    }
}
