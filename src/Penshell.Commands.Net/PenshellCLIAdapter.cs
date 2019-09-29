namespace Penshell.Commands.Net
{
    using System.Composition;
    using Dawn;
    using Microsoft.Extensions.DependencyInjection;
    using Penshell.Core;

    /// <summary>
    /// The adapter class for the penshell command line interface.
    /// </summary>
    [Export(typeof(IPenshellCLIAdapter))]
    public class PenshellCLIAdapter : IPenshellCLIAdapter
    {
        /// <inheritdoc />
        public void ConfigureServices(ServiceCollection services)
        {
            services.AddTransient<HttpGetCommand>();
        }

        /// <inheritdoc />
        public void RegisterCommandOptionValueConverters(PenshellCommandOptionValueConverterDictionary registry)
        {
            registry = Guard.Argument(registry).NotNull().Value;

            registry.Add(new UriConverter());
        }
    }
}
