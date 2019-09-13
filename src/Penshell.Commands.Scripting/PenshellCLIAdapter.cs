namespace Penshell.Commands.Scripting
{
    using System.Composition;
    using Microsoft.Extensions.DependencyInjection;
    using Penshell.Core;

    [Export(typeof(IPenshellCLIAdapter))]
    public class PenshellCLIAdapter : IPenshellCLIAdapter
    {
        public void ConfigureServices(ServiceCollection services)
        {
            services.AddTransient<RunCommand>();
        }
    }
}
