﻿using System;
using System.Linq;

namespace ConsoleAppNETCore
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Enter the folder which contains the folders with Plugins");
                string folderWithPlugins = Console.ReadLine();
                var manager = new Contracts.MEFPluginsManager(folderWithPlugins);
                Console.WriteLine("Plugins discovered are as follows:");
                string[] allAvailablePlugins =
                                manager.
                                PluginsWithMetaData.Select(p => p.Metadata["PluginName"] as string).
                                OrderBy(s => s).
                                ToArray();
                Console.WriteLine($"Found {allAvailablePlugins.Length} plugins");
                while (true)
                {
                    Console.WriteLine("-----------------------------------");
                    Console.WriteLine("Select a plugin to invoke");
                    foreach (string nameOfPlugin in allAvailablePlugins)
                    {
                        Console.WriteLine($"\t{nameOfPlugin}");
                    }
                    string choice = Console.ReadLine();
                    Contracts.ISomePlugin plugin = manager.CreatePlugin(choice);
                    if (plugin == null)
                    {
                        Console.WriteLine($"No plugin with name:'{choice}' was found");
                    }
                    else
                    {
                        try
                        {
                            plugin.DoWork();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
