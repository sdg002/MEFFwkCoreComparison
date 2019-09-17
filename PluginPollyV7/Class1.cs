using System;
using System.ComponentModel.Composition;
using System.Linq;

namespace PluginPollyV7
{
    [Export(typeof(Contracts.ISomePlugin))]
    [ExportMetadata("PluginName", "polly7")]
    public class Class1 : Contracts.ISomePlugin
    {
        public void DoWork()
        {
            string location = typeof(Polly.Context).Assembly.Location;
            string version = GetVersion(typeof(Polly.Context).Assembly);
            Console.WriteLine($"Inside method DoWork of {this.GetType().FullName}");
            Console.WriteLine($"Polly dll={location}");
            Console.WriteLine($"Polly version={version}");
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
