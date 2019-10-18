using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Loader;
using System.Text;

namespace ConsoleAppNetDepsJson
{
    /// <summary>
    /// Objective  - 
    ///     Preload the deps.json file(s) for each of the plugin assemblies
    ///     Handle the Resolving event
    ///     When request for an assembly arrives in the Resolving event, use the AssemblyDependencyResolver to find the matcing dependency assembly
    /// Challenges
    ///     .NET Core does not allow us to load 2 versions of the same assembly
    ///     .NET Core does not support the method ReflectionOnlyLoadFrom, therefore we cannot inspect the version and return the highest version from all folders
    /// 
    /// </summary>
    class FindAndLoadDependencies
    {
        private string[] _pathsPluginAssembly;
        List<AssemblyDependencyResolver> _lstResolvers;

        public FindAndLoadDependencies(string[] pathsAssemblies)
        {
            this._pathsPluginAssembly = pathsAssemblies;
            _lstResolvers = new List<AssemblyDependencyResolver>();
            foreach(string pathPluginAssembly in _pathsPluginAssembly)
            {
                string fileNameNoExtension = System.IO.Path.GetFileNameWithoutExtension(pathPluginAssembly);
                string folder= System.IO.Path.GetDirectoryName(pathPluginAssembly);
                string pathDepsJson = System.IO.Path.Combine(folder, fileNameNoExtension + ".deps.json");
                var resolver = new AssemblyDependencyResolver(pathDepsJson);
                _lstResolvers.Add(resolver);
            }
            AssemblyLoadContext.Default.Resolving += Default_Resolving;
        }
        private System.Reflection.Assembly Default_Resolving(AssemblyLoadContext arg1, System.Reflection.AssemblyName arg2)
        {
            Trace.WriteLine($"Default_Resolving event, {arg2}");
            var lstMatchingAssemblies = new List<System.Reflection.Assembly>();
            foreach (var resolver in _lstResolvers)
            {
                var pathAssembly = resolver.ResolveAssemblyToPath(arg2);
                if (pathAssembly == null) continue;

                //var reader = new System.Reflection.Metadata.MetadataReader() //too difficult - not sure how to fix this
                //    //https://github.com/dotnet/corefx/issues/41857
                //var definition =reader.GetAssemblyDefinition()

                var assem = System.Reflection.Assembly.LoadFile(pathAssembly);
                lstMatchingAssemblies.Add(assem);
            }
            //var assembliesHighedToLowestVersion = lstMatchingAssemblies.OrderByDescending(a => a.GetName().Version);
            //return assembliesHighedToLowestVersion.FirstOrDefault();
            var assembliesWithClosestMatchingVersion = 
                        lstMatchingAssemblies.
                        OrderBy(a => Math.Abs( arg2.Version.CompareTo(a.GetName().Version)) ).
                        ToArray();
            return assembliesWithClosestMatchingVersion.FirstOrDefault();
        }
        private System.Reflection.Assembly Default_Resolving_old(AssemblyLoadContext arg1, System.Reflection.AssemblyName arg2)
        {
            Trace.WriteLine($"Default_Resolving event, {arg2}");
            var lstMatchingAssemblies = new List<System.Reflection.Assembly>();
            foreach (var resolver in _lstResolvers)
            {
                var pathAssembly = resolver.ResolveAssemblyToPath(arg2);
                if (pathAssembly == null) continue;

                //var assem = AssemblyLoadContext.Default.LoadFromAssemblyPath(pathAssembly); //Does not allow loading 2 versions of Newtonsoft                
                //var assem = System.Reflection.Assembly.ReflectionOnlyLoadFrom(pathAssembly); //Operation is not supported on this platform

                var assem = System.Reflection.Assembly.LoadFrom(pathAssembly);
                //We cannot return the First assembly. We get NewtonSoft v9 ,when v12 was requested
                //return assem;
                lstMatchingAssemblies.Add(assem);
            }
            var assembliesHighedToLowestVersion = lstMatchingAssemblies.OrderByDescending(a => a.GetName().Version);
            return assembliesHighedToLowestVersion.FirstOrDefault();
        }
    }
}
