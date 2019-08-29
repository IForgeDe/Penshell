﻿namespace PenshellCLI
{
    using System;
    using System.Collections.Generic;
    using System.Composition.Hosting;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    using CliFx;
    using CliFx.Services;
    using Microsoft.Extensions.DependencyInjection;
    using Penshell.Core;
    using Serilog;
    using Serilog.Events;
    using Serilog.Sinks.SystemConsole.Themes;

    public static class Program
    {
        public static Task<int> Main(string[] args)
        {
            // configure logging
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Console(
                    outputTemplate: "[{Timestamp:HH:mm:ss.fff} {Level:u3}] {Message:lj}{NewLine}{Exception}",
                    theme: AnsiConsoleTheme.Code)
                .CreateLogger();

            // collect plugins via mef
            var commandAssemblies = new List<Assembly>();
            var configuration = new ContainerConfiguration();
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var baseDirectoryFiles = Directory.EnumerateFiles(baseDirectory, "*.Commands.*.dll", SearchOption.AllDirectories);
            foreach (var file in baseDirectoryFiles)
            {
                try
                {
                    var assembly = Assembly.LoadFrom(file);
                    configuration.WithAssembly(assembly);
                    commandAssemblies.Add(assembly);
                }
                catch (ReflectionTypeLoadException ex)
                {
                    Log.Error(ex, "An unexpected error occured while loading command assemblies.");
                }
                catch (BadImageFormatException ex)
                {
                    Log.Error(ex, "An unexpected error occured while loading command assemblies.");
                }
            }

            // collect commands
            var commandTypes = new List<Type>();
            var container = configuration.CreateContainer();
            var commandProviders = container.GetExports<IAssemblyCommandProvider>();
            foreach (var commandProvider in commandProviders)
            {
                commandTypes.AddRange(commandProvider.GetCommandTypes());
            }

            // test for commands
            if (!commandTypes.Any())
            {
                Log.Information("No nommands found.");
                return new TaskFactory<int>().StartNew(() => 0);
            }

            // HACK: Sorting via CommandSchemaResolver
            // Description: This could be part of the CliFx-Library
            // Example: CliApplicationBuilder().AddSchemaSort()
            var commandSchemaResolver = new CommandSchemaResolver();
            var commandSchemas = commandSchemaResolver.GetCommandSchemas(commandTypes);
            commandSchemas = commandSchemas.OrderBy(x => x.Name).ToArray();

            // prepare and build di container
            var services = new ServiceCollection();
            services.AddSingleton(Log.Logger);
            foreach (var commandSchema in commandSchemas)
            {
                services.AddTransient(commandSchema.Type);
            }

            // run commandline
            var serviceProvider = services.BuildServiceProvider();
            var cliApplication = new CliApplicationBuilder()
                .AddCommands(commandSchemas.Select(x => x.Type).ToArray())
                .AllowDebugMode(true)
                .AllowPreviewMode(true)
                .UseCommandFactory(schema => (ICommand)serviceProvider.GetRequiredService(schema.Type))
                .Build();
            return cliApplication.RunAsync(args);
        }
    }
}