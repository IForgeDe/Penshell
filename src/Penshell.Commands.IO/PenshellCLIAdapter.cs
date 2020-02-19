namespace Penshell.Commands.IO
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
                .AddTransient<CreateFileCommand>()
                .AddTransient<ReadFileCommand>()
                .AddTransient<WriteFileCommand>();
        }

        /// <inheritdoc />
        public IEnumerable<Command> CreateCommands(ServiceProvider serviceProvider)
        {
            var domainCommand = new Command("io");
            domainCommand.AddCommand(serviceProvider.GetService<CreateFileCommand>());
            domainCommand.AddCommand(serviceProvider.GetService<ReadFileCommand>());
            domainCommand.AddCommand(serviceProvider.GetService<WriteFileCommand>());
            var commandsList = new List<Command>
            {
                domainCommand,
            };
            return commandsList;
        }
    }
}
