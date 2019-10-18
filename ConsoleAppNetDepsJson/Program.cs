using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Loader;

namespace ConsoleAppNetDepsJson
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Diagnostics.Trace.Listeners.Add(new System.Diagnostics.ConsoleTraceListener());
            Console.WriteLine("Hello World!");
            string folderNewtonsoftV9 = @"PluginNewtonsoftv9\PluginNewtonsoftv9.dll";
            string folderNewtonsoftV12 = @"PluginNewtonsoftV12\PluginNewtonsoftv12.dll";
            string[] pathsAssembliesRelative = new string[] { folderNewtonsoftV9, folderNewtonsoftV12 };
            ///
            /// Get the absolute path to each of the plugins
            ///
            string folderRootPlugins = GetRootOfPlugins();
            var lstPluginPathsAbsolute = new List<string>();
            foreach (string pathRelativeAssembly in pathsAssembliesRelative)
            {
                string pathAbsoluteAssembly = System.IO.Path.Combine(folderRootPlugins, pathRelativeAssembly);
                lstPluginPathsAbsolute.Add(pathAbsoluteAssembly);
            }
            ///
            /// Setup the assembly resolver
            ///
            var finder = new FindAndLoadDependencies(lstPluginPathsAbsolute);
            ///
            /// Fire each of the plugins, from lowest version to highest. This is when we ecountered  failure in the traditional approach
            ///
            foreach (string pathAssemblyAbsolute in lstPluginPathsAbsolute)
            {
                string pathAbsoluteAssembly = System.IO.Path.Combine(folderRootPlugins, pathAssemblyAbsolute);
                Trace.WriteLine("-------------------------------------------------");
                Trace.WriteLine($"--- Testing with plugin=`{pathAbsoluteAssembly}`");
                Trace.WriteLine("-------------------------------------------------");
                var assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(pathAssemblyAbsolute);
                Type tTarget = assembly.GetTypes().FirstOrDefault(t => t.Name.ToLower() == "class1");
                var plugin = Activator.CreateInstance(tTarget) as Contracts.ISomePlugin;
                plugin.DoWork();
            }
        }
        private static string GetRootOfSolution()
        {
            string pathAssembly = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string folderAssembly = System.IO.Path.GetDirectoryName(pathAssembly);
            if (folderAssembly.EndsWith("\\") == false) folderAssembly = folderAssembly + "\\";
            string folderProjectLevel = System.IO.Path.GetFullPath(folderAssembly + "..\\..\\..\\..\\");
            return folderProjectLevel;

        }
        private static string GetRootOfPlugins()
        {
            string folderSolnRoot = GetRootOfSolution();
            string folderPluginsRoot = System.IO.Path.Combine(folderSolnRoot,"PluginsDeliveryFolder\\out");
            return folderPluginsRoot;
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
