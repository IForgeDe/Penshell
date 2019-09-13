namespace Penshell.Commands.Net
{
    using System.Composition;
    using FluentValidation;
    using Microsoft.Extensions.DependencyInjection;
    using Penshell.Core;

    [Export(typeof(IPenshellCLIAdapter))]
    public class PenshellCLIAdapter : IPenshellCLIAdapter
    {
        /// <inheritdoc />
        public void ConfigureServices(ServiceCollection services)
        {
            // commands
            services.AddTransient<HttpCommand>();

            // validators
            services.AddTransient<IValidator<HttpCommand>, HttpCommandValidator>();
        }

        /// <inheritdoc />
        public void RegisterCommandOptionValueConverters(PenshellCommandOptionValueConverterRegistry registry)
        {
            registry.Add(new UriConverter());
        }
    }
}
