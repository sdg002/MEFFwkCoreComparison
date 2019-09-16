using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Text;
using System.Linq;

namespace Contracts
{
    public class MEFPluginsManager
    {
        List<string> _loadedDllFiles = new List<string>();
        private string _folderWithPlugins;

        public MEFPluginsManager(string folderWithPlugins)
        {
            this._folderWithPlugins = folderWithPlugins;
            string[] subdirectories = System.IO.Directory.GetDirectories(folderWithPlugins);
            List<DirectoryCatalog> allDirCatalogs = new List<DirectoryCatalog>();
            foreach (string subdir in subdirectories)
            {
                var catalogDir = new DirectoryCatalog(subdir, "*plugin*.dll");
                allDirCatalogs.Add(catalogDir);
            }
            AggregateCatalog catalogFinal = new AggregateCatalog(allDirCatalogs);
            var container = new CompositionContainer(catalogFinal);
            container.ComposeParts(this);
            //testing - get loaded files
            foreach (var dirCat in allDirCatalogs)
            {
                var dllFilesThatWereDiscovered = dirCat.LoadedFiles;
                this.DiscoveredDllFiles.AddRange(dllFilesThatWereDiscovered);
            }
        }
        /// <summary>
        /// This will receive the lazy instances of all the discovered plugins
        /// </summary>
        [ImportMany]
        public IEnumerable<Lazy<Contracts.ISomePlugin, Dictionary<string, object>>> PluginsWithMetaData { get; set; }

        /// <summary>
        /// All DLLs that were discovered
        /// </summary>
        public List<string> DiscoveredDllFiles { get => _loadedDllFiles; }

        public ISomePlugin CreatePlugin(string name)
        {
            var lazy = this.PluginsWithMetaData.FirstOrDefault(p => (string)p.Metadata["PluginName"] == name);
            if (lazy == null) return null;
                //throw new InvalidOperationException($"No plugin with the name '{name}' was found");
            Contracts.ISomePlugin iplugin = lazy.Value;
            return iplugin;
        }
    }
}
