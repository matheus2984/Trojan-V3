using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using TroPlugin;

namespace Server.Plugins
{
    public static class PluginLoader
    {
        public static ICollection<IPlugin> LoadPlugins(string path, params object[] parameters)
        {
            string[] dllFileNames = null;

            if (Directory.Exists(path))
            {
                dllFileNames = Directory.GetFiles(path, "*.dll");

                ICollection<Assembly> assemblies = new List<Assembly>(dllFileNames.Length);
                foreach (string dllFile in dllFileNames)
                {
                    AssemblyName an = AssemblyName.GetAssemblyName(dllFile);
                    Assembly assembly = Assembly.Load(an);
                    assemblies.Add(assembly);
                }

                Type pluginType = typeof(IPlugin);
                ICollection<Type> pluginTypes = new List<Type>();
                foreach (Assembly assembly in assemblies)
                {
                    if (assembly != null)
                    {
                        Type[] types = assembly.GetTypes();

                        foreach (Type type in types)
                        {
                            if (type.IsInterface || type.IsAbstract)
                            {
                                continue;
                            }
                            else
                            {
                                if (type.GetInterface(pluginType.FullName) != null)
                                {
                                    pluginTypes.Add(type);
                                }
                            }
                        }
                    }
                }

                ICollection<IPlugin> plugins = new List<IPlugin>(pluginTypes.Count);
                foreach (Type type in pluginTypes)
                {
                    IPlugin plugin = (IPlugin)Activator.CreateInstance(type, parameters);
                    plugins.Add(plugin);
                }

                return plugins;
            }

            return null;
        }

        public static IPlugin Load(string dll, params object[] parameters)
        {
            Assembly pluginAssembly = Assembly.LoadFrom(dll);
            Type[] types = pluginAssembly.GetTypes();
            Type classType = null;
            foreach (Type type in types)
            {
                if (type.IsInterface || type.IsAbstract)
                {
                    continue;
                }
                else
                {
                    classType = type;
                }
            }

            return (IPlugin)Activator.CreateInstance(classType, parameters);
        }
    }
}