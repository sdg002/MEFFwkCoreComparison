using System;
using System.ComponentModel.Composition;
using System.Linq;

namespace PluginNewtonsoftV12
{
    [Export(typeof(Contracts.ISomePlugin))]
    [ExportMetadata("PluginName", "newtonv12")]
    public class Class1 : Contracts.ISomePlugin
    {
        public void DoWork()
        {
            string location = typeof(Newtonsoft.Json.JsonConvert).Assembly.Location;
            string version = GetVersion(typeof(Newtonsoft.Json.JsonConvert).Assembly);
            Console.WriteLine($"Inside method DoWork of {this.GetType().FullName}");
            Console.WriteLine($"Newtonsoft={location}");
            Console.WriteLine($"Newtonsoft version={version}");
        }
        internal string GetVersion(System.Reflection.Assembly thisAssembly)
        {
            var attFileVer = thisAssembly.
                        GetCustomAttributes(typeof(System.Reflection.AssemblyFileVersionAttribute), false).First() as
                            System.Reflection.AssemblyFileVersionAttribute;
            if (attFileVer != null)
                return attFileVer.Version;
            else
            {
                return null;
            }
        }
    }
}
