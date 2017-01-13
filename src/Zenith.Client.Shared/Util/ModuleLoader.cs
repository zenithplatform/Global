using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Zenith.Client.Shared.Interfaces;

namespace Zenith.Client.Shared.Util
{
    public class ModuleLoader
    {
        private static volatile ModuleLoader _moduleLoader;
        private static readonly object _syncObject = new object();
        static string ResolutionPath = @"D:\Programming\Astronomy\Dev\Zenith\src\Client\Zenith.WorkspaceModule\bin\Debug";

        private ModuleLoader()
        {
            AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += ResolveAssembly;
            AppDomain.CurrentDomain.AssemblyResolve += ResolveAssembly;
        }

        public static ModuleLoader Instance
        {
            get
            {
                if (null == _moduleLoader)
                {
                    lock (_syncObject)
                    {
                        if (null == _moduleLoader)
                        {
                            _moduleLoader = new ModuleLoader();
                        }
                    }
                }

                return _moduleLoader;
            }
        }

        public IModuleInitializer LoadModule(string path)
        {
            Assembly assembly = Assembly.LoadFrom(path);

            if (assembly != null)
            {
                Type metadataType = assembly.GetTypes().WhichImplements<IModuleInitializer>();
                IModuleInitializer initializer = Activator.CreateInstance(metadataType) as IModuleInitializer;
                initializer.InitModule();

                return initializer;
            }

            return null;
        }

        private static Assembly ResolveAssembly(object sender, ResolveEventArgs args)
        {
            Assembly[] currentAssemblies = AppDomain.CurrentDomain.GetAssemblies();

            for (int i = 0; i < currentAssemblies.Length; i++)
            {
                if (currentAssemblies[i].FullName == args.Name)
                {
                    return currentAssemblies[i];
                }
            }

            return FindAssembliesInDirectory(args.Name, ResolutionPath);
        }

        private static Assembly FindAssembliesInDirectory(string assemblyName, string directory)
        {
            foreach (string file in Directory.GetFiles(directory))
            {
                Assembly assm;

                if (TryLoadAssemblyFromFile(file, assemblyName, out assm))
                    return assm;
            }

            return null;
        }

        private static bool TryLoadAssemblyFromFile(string file, string assemblyName, out Assembly assm)
        {
            try
            {
                // Convert the filename into an absolute file name for 
                // use with LoadFile. 
                file = new FileInfo(file).FullName;

                if (AssemblyName.GetAssemblyName(file).FullName == assemblyName)
                {
                    assm = Assembly.LoadFile(file);
                    return true;
                }
            }
            catch
            {
                /* Do Nothing */
            }
            assm = null;
            return false;
        }
    }
}
