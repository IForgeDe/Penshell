namespace Penshell.Commands.Scripting
{
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
        public void ConfigureServices(ServiceCollection services)
        {
            services
                .AddTransient<ExitCommand>()
                .AddTransient<RunCommand>();
        }
    }
}
