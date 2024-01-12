using System;
using System.Collections.Generic;
using System.Reflection;

namespace Assets.Scripts.Core.Events
{
    public static class PredefineAssemblyUtil
    {
        enum AssemblyType
        {
            AssemblyCSharp,
            AssemblyCSharpEditor,
            AssemblyCSharpFirstPass,
            AssemblyCSharpEditorFirstPass
        }

        static AssemblyType? GetAssemblyType(string assemblyName)
        {
            return assemblyName switch
            {
                "Assembly-CSharp" => AssemblyType.AssemblyCSharp,
                "Assembly-CSharp-Editor" => AssemblyType.AssemblyCSharpEditor,
                "Assembly-CSharp-firstpass" => AssemblyType.AssemblyCSharpFirstPass,
                "Assembly-CSharp-Editor-firstpass" => AssemblyType.AssemblyCSharpEditorFirstPass,
                _ => null
            };
        }

        /// <summary>
        /// Add types from an assembly to a list of types if they are assignable from the interface type provided in parameter.
        /// Goal : Get all types that implement an interface from all assemblies.
        /// </summary>
        /// <param name="assembly"></param>
        /// <param name="types"></param>
        /// <param name="interfaceType"></param>
        static void AddTypesFromAssembly(Type[] assembly, ICollection<Type> types, Type interfaceType)
        {
            if(assembly == null) return;

            foreach (Type type in assembly)
            {
                if (type != interfaceType && interfaceType.IsAssignableFrom(type))
                    types.Add(type);

            }
        }

        /// <summary>
        /// Get all types that implement an interface from all assemblies.
        /// </summary>
        /// <param name="interfaceType"></param>
        /// <returns></returns>
        public static List<Type> GetTypes(Type interfaceType)
        {
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

            Dictionary<AssemblyType, Type[]> assemblyTypes = new Dictionary<AssemblyType, Type[]>();
            List<Type> types = new List<Type>();
            foreach (Assembly assembly in assemblies)
            {
                AssemblyType? assemblyType = GetAssemblyType(assembly.GetName().Name);
                if (assemblyType != null)
                {
                    assemblyTypes.Add((AssemblyType)assemblyType, assembly.GetTypes());
                }
            }

            AddTypesFromAssembly(assemblyTypes[AssemblyType.AssemblyCSharp], types, interfaceType);
            AddTypesFromAssembly(assemblyTypes[AssemblyType.AssemblyCSharpFirstPass], types, interfaceType);

            return types;
        }
    }
}