namespace Penshell.Core
{
    using System.Collections.Generic;
    using System.CommandLine;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// This interface defines the possible and optional methods to adapt an assembly to a penshell command line interface.
    /// </summary>
    public interface IPenshellCLIAdapter
    {
        /// <summary>
        /// The method to configure the di container services.
        /// </summary>
        /// <example>
        /// <code>
        /// services.AddTransient{ExampleCommand}();
        /// </code>
        /// </example>
        /// <param name="services">The service instance, where the injections can be parametrized.</param>
        void ConfigureServices(IServiceCollection services);

        /// <summary>
        /// Creates the commands.
        /// </summary>
        /// <param name="serviceProvider">The <see cref="ServiceProvider"/> instance.</param>
        /// <returns>The enumerable <see cref="ICommand"/> instance.</returns>
        IEnumerable<Command> CreateCommands(ServiceProvider serviceProvider);
    }
}
