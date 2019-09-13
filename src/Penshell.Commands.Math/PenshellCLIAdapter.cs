namespace Penshell.Commands.Math
{
    using System.Composition;
    using Microsoft.Extensions.DependencyInjection;
    using Penshell.Core;

    [Export(typeof(IPenshellCLIAdapter))]
    public class PenshellCLIAdapter : IPenshellCLIAdapter
    {
        public void ConfigureServices(ServiceCollection services)
        {
            services
                .AddTransient<AbsCommand>()
                .AddTransient<AddCommand>()
                .AddTransient<DivideCommand>()
                .AddTransient<MultiplyCommand>()
                .AddTransient<SubstractCommand>();
        }
    }
}
