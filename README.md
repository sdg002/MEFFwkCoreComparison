# MEFFwkCoreComparison
Managed Extensibility Framework  - .NET Framework and .NET Core differences

# Overview
Demontrates assembly load failure when using MEF on .NET Core 2.1. This is the sample code which I wrote while logging this issue with Github.

# Github issue
https://github.com/dotnet/core/issues/3376

# Summary of the problem
.NET Framework works as expected. It is able to load the 2 versions of Newtonsoft side by side. However, .NET Core is unable to cope with the 2 versions side by side. I wonder if this is a limitation of .NET Core?

# How to run this sample?

- Clone this project
- Do a build all
- Verify that assemblies from the project PluginNewtonsoftv12 and PluginNewtonsoftv9 are copied over to the PluginsDeliveryFolder\Out
- Execute the EXE from the project ConsoleAppNetFramework . Select the plugin newtonv12 and then newtonv9. Notice the path to the assemblies Newtonsoft.dll. You should see 2 versions loaded from the two folders of the plugin assemblies
- Execute the EXE from the project ConsoleAppNetCore. Select newtonv12 followed by newtonv9. This will work. Run the EXE again. Now select newtonv9 and then newtonv12. You should see an assembly load failure exception
