namespace Penshell.Commands.Process
{
    using System.Collections.Generic;
    using System.CommandLine;
    using System.Composition;
    using Microsoft.Extensions.DependencyInjection;
    using Penshell.Core;

    /// <summary>
    /// The adapter class for the penshell command line interface.
    /// </summary>
    [Export(typeof(IPenshellCLIAdapter))]
    public class PenshellCLIAdapter : IPenshellCLIAdapter
    {
        /// <inheritdoc />
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddTransient<OpenBrowserCommand>()
                .AddTransient<SleepCommand>()
                .AddTransient<StartCommand>();
        }

        /// <inheritdoc />
        public IEnumerable<Command> CreateCommands(ServiceProvider serviceProvider)
        {
            var domainCommand = new Command("process");
            domainCommand.AddCommand(serviceProvider.GetService<OpenBrowserCommand>());
            domainCommand.AddCommand(serviceProvider.GetService<SleepCommand>());
            domainCommand.AddCommand(serviceProvider.GetService<StartCommand>());
            var commandsList = new List<Command>
            {
                domainCommand,
            };
            return commandsList;
        }
    }
}
