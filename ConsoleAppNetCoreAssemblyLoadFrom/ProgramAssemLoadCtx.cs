using System;
using System.Linq;
using System.Runtime.Loader;

namespace ConsoleAppNetCoreAssemblyLoadFrom
{
    class ProgramAssemLoadCtx
    {
        static void Main(string[] args)
        {
            try
            {
                string folderNewtonsoftV9 = @"C:\Users\saurabhd\MyTrials\OtherStuff\MEFFwkCoreComparison\PluginsDeliveryFolder\out\PluginNewtonsoftv9\PluginNewtonsoftv9.dll";
                string folderNewtonsoftV12 = @"C:\Users\saurabhd\MyTrials\OtherStuff\MEFFwkCoreComparison\PluginsDeliveryFolder\out\PluginNewtonsoftV12\PluginNewtonsoftv12.dll";
                string[] pathsAssemblies = new string[] { folderNewtonsoftV9, folderNewtonsoftV12 };
                foreach (string pathAssembly in pathsAssemblies)
                {

                    var assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(pathAssembly);
                    Type tTarget = assembly.GetTypes().FirstOrDefault(t => t.Name.ToLower() == "class1");
                    var plugin = Activator.CreateInstance(tTarget) as Contracts.ISomePlugin;
                    plugin.DoWork();

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
