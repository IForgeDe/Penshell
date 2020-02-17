namespace PenshellCLI
{
    using System;
    using System.Collections.Generic;
    using System.CommandLine;
    using System.CommandLine.IO;
    using System.Composition.Hosting;
    using System.Globalization;
    using System.IO;
    using System.Reflection;
    using Microsoft.Extensions.DependencyInjection;
    using Penshell.Core;
    using Serilog;
    using Serilog.Events;
    using Serilog.Sinks.SystemConsole.Themes;

    /// <summary>
    /// The main programm class.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Gets or sets the static <see cref="ServiceProvider"/> instance.
        /// </summary>
        private static ServiceProvider? ServiceProvider { get; set; } = null;

        /// <summary>
        /// Gets the <see cref="IServiceCollection"/> instance.
        /// </summary>
        private static IServiceCollection Services { get; } = new ServiceCollection();

        ///// <summary>
        ///// The main entry point of the program.
        ///// </summary>
        ///// <param name="args">The string array of the command line arguments.</param>
        ///// <returns>The generic Task of int, defining the exit code of this console application.</returns>
        ////public static Task<int> Main(string[] args)
        ////{
        ////    // reajust culture to unify in and out
        ////    CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;

        ////    // configure logging
        ////    Log.Logger = new LoggerConfiguration()
        ////        .MinimumLevel.Debug()
        ////        .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
        ////        .Enrich.FromLogContext()
        ////        .WriteTo.Console(
        ////            outputTemplate: "[{Timestamp:HH:mm:ss.fff} {Level:u3}] {Message:lj}{NewLine}{Exception}",
        ////            theme: AnsiConsoleTheme.Code)
        ////        .CreateLogger();

        ////    // collect plugins via mef
        ////    var commandAssemblies = new List<Assembly>();
        ////    var configuration = new ContainerConfiguration();
        ////    var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        ////    var baseDirectoryFiles = Directory.EnumerateFiles(baseDirectory, "*.Commands.*.dll", SearchOption.AllDirectories);
        ////    foreach (var file in baseDirectoryFiles)
        ////    {
        ////        try
        ////        {
        ////            var assembly = Assembly.LoadFrom(file);
        ////            configuration.WithAssembly(assembly);
        ////            commandAssemblies.Add(assembly);
        ////        }
        ////        catch (ReflectionTypeLoadException ex)
        ////        {
        ////            Log.Error(ex, "An unexpected error occured while loading command assemblies.");
        ////        }
        ////        catch (BadImageFormatException ex)
        ////        {
        ////            Log.Error(ex, "An unexpected error occured while loading command assemblies.");
        ////        }
        ////    }

        ////    // prepare di container
        ////    var services = new ServiceCollection();
        ////    CommandOptionValueConverterRegistry = new PenshellCommandOptionValueConverterDictionary(Log.Logger);
        ////    CommandOptionInputConverter = new PenshellCommandOptionInputConverter(CommandOptionValueConverterRegistry);
        ////    services.AddSingleton(Log.Logger);
        ////    services.AddSingleton(CommandOptionValueConverterRegistry);
        ////    services.AddSingleton(CommandOptionInputConverter);

        ////    // manage services and other injections
        ////    var container = configuration.CreateContainer();
        ////    var cliAdapters = container.GetExports<IPenshellCLIAdapter>();
        ////    foreach (var cliAdapter in cliAdapters)
        ////    {
        ////        cliAdapter.ConfigureServices(services);
        ////        cliAdapter.RegisterCommandOptionValueConverters(CommandOptionValueConverterRegistry);
        ////    }

        ////    // test for commands
        ////    var commandTypes = services.Where(x => x.ServiceType.GetInterfaces().Contains(typeof(ICommand))).Select(x => x.ImplementationType).ToArray();
        ////    if (!commandTypes.Any())
        ////    {
        ////        Log.Information("No nommands found.");
        ////        return new TaskFactory<int>().StartNew(() => 0);
        ////    }

        ////    // HACK: Sorting via CommandSchemaResolver
        ////    // Description: This could be part of the CliFx-Library
        ////    // Example: CliApplicationBuilder().AddSchemaSort()
        ////    var commandSchemaResolver = new CommandSchemaResolver();
        ////    var commandSchemas = commandSchemaResolver.GetCommandSchemas(commandTypes);
        ////    commandSchemas = commandSchemas.OrderBy(x => x.Name).ToArray();
        ////    foreach (var commandSchema in commandSchemas)
        ////    {
        ////        services.AddTransient(commandSchema.Type);
        ////    }

        ////    // prepare command registry
        ////    var commandRegistry = new PenshellCommandRegistry();
        ////    commandRegistry.Register(commandSchemas);
        ////    services.AddSingleton(commandRegistry);

        ////    // prepare console
        ////    var console = PenshellConsoleBuilder.Build();

        ////    // build command factories
        ////    var commandFactoryMethod = new Func<CommandSchema, ICommand>(schema => (ICommand)ServiceProvider.GetRequiredService(schema.Type));
        ////    var commandFactory = new DelegateCommandFactory(commandFactoryMethod);
        ////    services.AddSingleton<ICommandFactory>(commandFactory);

        ////    // build di container
        ////    ServiceProvider = services.BuildServiceProvider();

        ////    // build and run application
        ////    var cliApplication = new CliApplicationBuilder()
        ////        .UseConsole(console)
        ////        .AddCommands(commandSchemas.Select(x => x.Type).ToArray())
        ////        .AllowDebugMode(true)
        ////        .AllowPreviewMode(true)
        ////        .UseCommandFactory(commandFactoryMethod)
        ////        .UseCommandOptionInputConverter(CommandOptionInputConverter)
        ////        .Build();
        ////    return cliApplication.RunAsync(args);
        ////}

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
            var rootCommand = new RootCommand("Welcome to the penshell root command. This command is useless.");
            foreach (var adapterRootCommand in adapterRootCommands)
            {
                rootCommand.AddCommand(adapterRootCommand);
            }

            // Parse the incoming args and invoke the handler
            return rootCommand.InvokeAsync(args).Result;
        }

        /// <summary>
        /// Configures the services.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> instance.</param>
        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IFormatProvider>((_) => CultureInfo.InvariantCulture);
            services.AddTransient<IConsole, SystemConsole>();
            services.AddTransient((_) => Log.Logger);
        }
    }
}
