namespace Penshell.Commands.Math
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
                .AddTransient<AbsCommand>()
                .AddTransient<AddCommand>()
                .AddTransient<DivideCommand>()
                .AddTransient<MultiplyCommand>()
                .AddTransient<SubstractCommand>();
        }

        /// <inheritdoc />
        public IEnumerable<Command> CreateCommands(ServiceProvider serviceProvider)
        {
            var domainCommand = new Command("math");
            domainCommand.AddCommand(serviceProvider.GetService<AbsCommand>());
            domainCommand.AddCommand(serviceProvider.GetService<AddCommand>());
            domainCommand.AddCommand(serviceProvider.GetService<DivideCommand>());
            domainCommand.AddCommand(serviceProvider.GetService<MultiplyCommand>());
            var commandsList = new List<Command>
            {
                domainCommand,
            };
            return commandsList;
        }
    }
}
