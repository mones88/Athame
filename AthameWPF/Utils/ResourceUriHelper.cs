using System;
using System.Reflection;

namespace AthameWPF.Utils
{
    public static class ResourceUriHelper
    {
        private static readonly string AssemblyName = Assembly.GetExecutingAssembly().GetName().Name;

        public static Uri BuildUri(string resourceName)
        {
            return new Uri($"pack://application:,,,/{AssemblyName};component/Resources/{resourceName}", UriKind.Absolute);
        }
    }
}
