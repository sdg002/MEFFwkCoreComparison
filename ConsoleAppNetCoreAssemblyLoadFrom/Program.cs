using System;
using System.Linq;

namespace ConsoleAppNetCoreAssemblyLoadFrom
{
    class Program
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
                    var assembly = System.Reflection.Assembly.LoadFrom(pathAssembly);
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
