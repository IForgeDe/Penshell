namespace Penshell.Commands.Net
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
            services.AddTransient<HttpGetCommand>();
        }

        /// <inheritdoc />
        public IEnumerable<Command> CreateCommands(ServiceProvider serviceProvider)
        {
            var domainCommand = new Command("net");
            domainCommand.AddCommand(serviceProvider.GetService<HttpGetCommand>());
            var commandsList = new List<Command>
            {
                domainCommand,
            };
            return commandsList;
        }
    }
}
