using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GetPathToProjectDir()
        {
            string path = Util.GetUnitTestProjectRootDirectory();
        }
        [TestMethod]
        public void GetPathToSolutionDir()
        {
            string path = Util.GetSolutionRootDirectory();
        }
        [TestMethod]
        public void GetPathToPluginsRoot()
        {
            string path = Util.GetPluginRootDirectory();

        }
        [TestMethod]
        public void AssemblyDepResolver_Load_V12_using_depsjson_V12()
        {
            string pathDepsJsonV12 = System.IO.Path.Combine(Util.GetPluginRootDirectory(), "PluginNewtonsoftV12", "PluginNewtonsoftV12.deps.json");
            var resolver = new System.Runtime.Loader.AssemblyDependencyResolver(pathDepsJsonV12);
            var assemNameNewtonsoftV9 = new System.Reflection.AssemblyName("Newtonsoft.Json, Version=12.0.0.0, Culture=neutral, PublicKeyToken=30ad4fe6b2a6aeed");
            var path=resolver.ResolveAssemblyToPath(assemNameNewtonsoftV9);
            Trace.WriteLine($"Path to Newtonsoft={path}");
            Assert.IsNotNull(path);
        }
        [TestMethod]
        public void AssemblyDepResolver_Load_V9_using_depsjson_V12()
        {
            string pathDepsJsonV12 = System.IO.Path.Combine(Util.GetPluginRootDirectory(), "PluginNewtonsoftV12", "PluginNewtonsoftV12.deps.json");
            var resolver = new System.Runtime.Loader.AssemblyDependencyResolver(pathDepsJsonV12);
            var assemNameNewtonsoftV9 = new System.Reflection.AssemblyName("Newtonsoft.Json, Version=9.0.0.0, Culture=neutral, PublicKeyToken=30ad4fe6b2a6aeed");
            var path = resolver.ResolveAssemblyToPath(assemNameNewtonsoftV9);
            Trace.WriteLine($"Path to Newtonsoft={path}");
            Assert.IsNotNull(path);
        }
        [TestMethod]
        public void AssemblyDepResolver_Load_V12_different_publickey_using_depsjson_V12()
        {
            string pathDepsJsonV12 = System.IO.Path.Combine(Util.GetPluginRootDirectory(), "PluginNewtonsoftV12", "PluginNewtonsoftV12.deps.json");
            var resolver = new System.Runtime.Loader.AssemblyDependencyResolver(pathDepsJsonV12);
            var assemNameNewtonsoftV9 = new System.Reflection.AssemblyName("Newtonsoft.Json, Version=12.0.0.0, Culture=neutral, PublicKeyToken=ddadddddb2a6aeed");
            var path = resolver.ResolveAssemblyToPath(assemNameNewtonsoftV9);
            Trace.WriteLine($"Path to Newtonsoft={path}");
            Assert.IsNull(path);
        }
        [TestMethod]
        public void AssemblyDepResolver_Load_V9_different_publickey_using_depsjson_V12()
        {
            string pathDepsJsonV12 = System.IO.Path.Combine(Util.GetPluginRootDirectory(), "PluginNewtonsoftV12", "PluginNewtonsoftV12.deps.json");
            var resolver = new System.Runtime.Loader.AssemblyDependencyResolver(pathDepsJsonV12);
            var assemNameNewtonsoftV9 = new System.Reflection.AssemblyName("Newtonsoft.Json, Version=9.0.0.0, Culture=neutral, PublicKeyToken=ddadddddb2a6aeed");
            var path = resolver.ResolveAssemblyToPath(assemNameNewtonsoftV9);
            Trace.WriteLine($"Path to Newtonsoft={path}");
            Assert.IsNull(path);
        }
    }
}
