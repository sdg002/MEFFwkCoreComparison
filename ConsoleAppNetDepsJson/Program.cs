using System;
using System.Linq;
using System.Runtime.Loader;

namespace ConsoleAppNetDepsJson
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            string folderNewtonsoftV9 = @"C:\Users\saurabhd\MyTrials\OtherStuff\MEFFwkCoreComparison\PluginsDeliveryFolder\out\PluginNewtonsoftv9\PluginNewtonsoftv9.dll";
            string folderNewtonsoftV12 = @"C:\Users\saurabhd\MyTrials\OtherStuff\MEFFwkCoreComparison\PluginsDeliveryFolder\out\PluginNewtonsoftV12\PluginNewtonsoftv12.dll";
            string[] pathsAssemblies = new string[] { folderNewtonsoftV9, folderNewtonsoftV12 };
            FindAndLoadDependencies finder = new FindAndLoadDependencies(pathsAssemblies);

            foreach (string pathAssembly in pathsAssemblies)
            {
                var assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(pathAssembly);
                Type tTarget = assembly.GetTypes().FirstOrDefault(t => t.Name.ToLower() == "class1");
                var plugin = Activator.CreateInstance(tTarget) as Contracts.ISomePlugin;
                plugin.DoWork();
            }
        }
    }
}



/////
/////.NET Core will not allow this
////var a9 = System.Reflection.Assembly.LoadFrom(@"C:\Users\saurabhd\MyTrials\OtherStuff\MEFFwkCoreComparison\PluginsDeliveryFolder\out\PluginNewtonsoftv9\Newtonsoft.Json.dll");
////var a12 = System.Reflection.Assembly.LoadFrom(@"C:\Users\saurabhd\MyTrials\OtherStuff\MEFFwkCoreComparison\PluginsDeliveryFolder\out\PluginNewtonsoftV12\Newtonsoft.Json.dll");

////var a12 = AssemblyLoadContext.Default.LoadFromAssemblyPath(@"C:\Users\saurabhd\MyTrials\OtherStuff\MEFFwkCoreComparison\PluginsDeliveryFolder\out\PluginNewtonsoftV12\Newtonsoft.Json.dll");
////var a9 = AssemblyLoadContext.Default.LoadFromAssemblyPath(@"C:\Users\saurabhd\MyTrials\OtherStuff\MEFFwkCoreComparison\PluginsDeliveryFolder\out\PluginNewtonsoftv9\Newtonsoft.Json.dll");
///*
//System.IO.FileLoadException
//HResult=0x80131621
//Message=Assembly with same name is already loaded
//Source=<Cannot evaluate the exception source>
//StackTrace:             
// */

/////
