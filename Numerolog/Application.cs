using System.Reflection;

namespace Numerolog
{
    public static class Application
    {
        private static readonly AssemblyName assemblyName = Assembly.GetEntryAssembly()!.GetName();

        public static readonly string Name = assemblyName.Name!;
        public static readonly string Version = assemblyName.Version!.ToString(3);
    }
}
