namespace PenshellCLI
{
    using System;
    using System.Collections.Generic;
    using System.CommandLine;
    using System.Composition.Hosting;
    using System.IO;
    using System.Reflection;
    using Microsoft.Extensions.DependencyInjection;
    using Penshell.Core;
    using Penshell.Core.Console;
    using Serilog;
    using Serilog.Events;
    using Serilog.Sinks.SystemConsole.Themes;

    /// <summary>
    /// The main programm class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Gets the <see cref="PenshellCommandRegistry"/> instance.
        /// </summary>
        private static PenshellCommandRegistry Registry { get; } = new PenshellCommandRegistry();

        /// <summary>
        /// Gets or sets the static <see cref="ServiceProvider"/> instance.
        /// </summary>
        private static ServiceProvider? ServiceProvider { get; set; } = null;

        /// <summary>
        /// Gets the <see cref="IServiceCollection"/> instance.
        /// </summary>
        private static IServiceCollection Services { get; } = new ServiceCollection();

        /// <summary>
        /// The main entry point of the program.
        /// </summary>
        /// <param name="args">The string array of the command line arguments.</param>
        /// <returns>The int, defining the exit code of this console application.</returns>
        public static int Main(string[] args)
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

            // configure base services
            ConfigureServices(Services);

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

            // manage services and other injections
            var compositeHost = configuration.CreateContainer();
            var cliAdapters = compositeHost.GetExports<IPenshellCLIAdapter>();
            var adapterRootCommands = new List<Command>();
            foreach (var cliAdapter in cliAdapters)
            {
                cliAdapter.ConfigureServices(Services);
            }

            // build di container
            ServiceProvider = Services.BuildServiceProvider();

            // create commands via di container
            foreach (var cliAdapter in cliAdapters)
            {
                adapterRootCommands.AddRange(cliAdapter.CreateCommands(ServiceProvider));
            }

            // build command structure
            var rootCommand = new RootCommand("Welcome to the penshell cli.");
            foreach (var adapterRootCommand in adapterRootCommands)
            {
                rootCommand.AddCommand(adapterRootCommand);
            }

            // register command for later usage
            Registry.Register(rootCommand);

            // Parse the incoming args and invoke the handler
            return rootCommand.InvokeAsync(args).Result;
        }

        /// <summary>
        /// Configures the services.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> instance.</param>
        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton((_) => new PenshellConsoleBuilder().Build());
            services.AddTransient((_) => Log.Logger);
            services.AddSingleton((_) => Registry);
        }
    }
}
