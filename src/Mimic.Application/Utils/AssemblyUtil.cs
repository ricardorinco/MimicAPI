using System;
using System.Linq;
using System.Reflection;

namespace Mimic.Application.Utils
{
    public static class AssemblyUtil
    {
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
