using System;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace AutoTest.Exceptions
{
    /// <summary>
    /// When running all unittests, serialization fails.
    /// Found this article and it seems to work
    /// https://social.msdn.microsoft.com/Forums/vstudio/en-US/e5f0c371-b900-41d8-9a5b-1052739f2521/deserialize-unable-to-find-an-assembly-?forum=netfxbcl
    /// </summary>
    internal class Binder : SerializationBinder
    {
        public override Type BindToType(string assemblyName, string typeName)
        {
            string shortAssemblyName = assemblyName.Split(',')[0];
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

            Type type = assemblies.Where(assembly => shortAssemblyName == assembly.FullName.Split(',')[0])
                .Select(assembly => assembly.GetType(typeName)).FirstOrDefault();

            return type;
        }
    }
}

