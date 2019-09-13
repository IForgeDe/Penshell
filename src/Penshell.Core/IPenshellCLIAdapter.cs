namespace Penshell.Core
{
    using Microsoft.Extensions.DependencyInjection;

    public interface IPenshellCLIAdapter
    {
        void ConfigureServices(ServiceCollection services);

        void RegisterCommandOptionValueConverters(PenshellCommandOptionValueConverterRegistry registry)
        {
        }
    }
}
