namespace Penshell.Core
{
    using System;
    using System.Collections.Generic;
    using Dawn;
    using Serilog;

    public class PenshellCommandOptionValueConverterDictionary : Dictionary<Type, IPenshellCommandOptionValueConverter>, IPenshellCommandOptionValueConverterDictionary
    {
        private readonly ILogger _logger;

        public PenshellCommandOptionValueConverterDictionary(ILogger logger)
        {
            _logger = Guard.Argument(logger).NotNull().Value;
        }

        public void Add(IPenshellCommandOptionValueConverter converter)
        {
            converter = Guard.Argument(converter).NotNull().Value;

            if (this.ContainsKey(converter.TargetType))
            {
                _logger.Error($"Multiple option value converter found for type '{converter.TargetType}'. Keeping first one.");
                return;
            }

            this.Add(converter.TargetType, converter);
        }
    }
}
