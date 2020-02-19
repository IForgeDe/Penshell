namespace Penshell.Commands.Scripting
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
                .AddTransient<ExitCommand>()
                .AddTransient<RunCommand>();
        }

        /// <inheritdoc />
        public IEnumerable<Command> CreateCommands(ServiceProvider serviceProvider)
        {
            var domainCommand = new Command("script");
            domainCommand.AddCommand(serviceProvider.GetService<ExitCommand>());
            domainCommand.AddCommand(serviceProvider.GetService<RunCommand>());
            var commandsList = new List<Command>
            {
                domainCommand,
            };
            return commandsList;
        }
    }
}
