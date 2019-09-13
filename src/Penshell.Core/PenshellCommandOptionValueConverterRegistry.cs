namespace Penshell.Core
{
    using System;
    using System.Collections.Generic;
    using Serilog;

    public class PenshellCommandOptionValueConverterRegistry : Dictionary<Type, IPenshellCommandOptionValueConverter>, IPenshellCommandOptionValueConverterRegistry
    {
        private readonly ILogger _logger;

        public PenshellCommandOptionValueConverterRegistry(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void Add(IPenshellCommandOptionValueConverter converter)
        {
            if (this.ContainsKey(converter.TargetType))
            {
                _logger.Error($"Multiple option value converter found for type '{converter.TargetType}'. Keeping first one.");
                return;
            }

            this.Add(converter.TargetType, converter);
        }
    }
}
