# MEFFwkCoreComparison
Managed Extensibility Framework  - .NET Framework and .NET Core differences

# Scenario
We have a simple .NET Core EXE which attempts to load two different plugin class libraries through a contractual interface. The assemblies of the plugins and their dependencies reside in their respective folders. The only binding information between the plugin assemblies and the EXE is the Contracts library. The plugin projects have the attribute CopyLocalLockFile attribute set in their CSPROJ so that all dependencies get copied over.

1. Plugin A references Newtonsoft V9
2. Plugin B references Newtonsoft V9

### Problem
- When the EXE tried to load Plugin A, it works. But, if it attemps to load Plugin B, then it fails to find Newtonsoft v12 (file not found exception).
- However, if the EXE loads Plugin B first then it works and it can also load Plugin A without any issues




# Github issue
https://github.com/dotnet/core/issues/3376

# Overview
Demontrates the assembly load failures in the following scenarios

1. When using MEF on .NET Core 3.0. This was the original problem I faced on .NET Core.
2. When using `Assembly.LoadFrom` method. I wanted to verify if MEF is the culprit of is the root cause at a more fundamental level


# Summary of the problem
.NET Framework works as expected. It is able to load the 2 versions of Newtonsoft side by side. However, .NET Core is unable to cope with the 2 versions side by side. I wonder if this is a limitation of .NET Core?

# How to run this sample?

- Clone this project
- Do a build all
- Verify that assemblies from the project PluginNewtonsoftv12 and PluginNewtonsoftv9 are copied over to the PluginsDeliveryFolder\Out
- Execute the EXE from the project ConsoleAppNetFramework . Select the plugin newtonv12 and then newtonv9. Notice the path to the assemblies Newtonsoft.dll. You should see 2 versions loaded from the two folders of the plugin assemblies
- Execute the EXE from the project ConsoleAppNetCore. Select newtonv12 followed by newtonv9. This will work. Run the EXE again. Now select newtonv9 and then newtonv12. You should see an assembly load failure exception
