namespace Penshell.Core
{
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
        void ConfigureServices(ServiceCollection services);

        /// <summary>
        /// This optionally method is called via mef to allow registering of specific command option value converters.
        /// </summary>
        /// <example>
        /// <code>
        /// registry.Add(new ExampleConverter());
        /// </code>
        /// </example>
        /// <param name="registry">The registry instance.</param>
        void RegisterCommandOptionValueConverters(PenshellCommandOptionValueConverterDictionary registry)
        {
        }
    }
}
