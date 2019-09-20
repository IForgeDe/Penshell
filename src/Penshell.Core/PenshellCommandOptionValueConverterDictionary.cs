namespace Penshell.Core
{
    using System;
    using System.Collections.Generic;
    using Dawn;
    using Serilog;

    /// <summary>
    /// Default implementation of the <see cref="IPenshellCommandOptionValueConverterDictionary"/> interface.
    /// </summary>
    public class PenshellCommandOptionValueConverterDictionary : Dictionary<Type, IPenshellCommandOptionValueConverter>, IPenshellCommandOptionValueConverterDictionary
    {
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="PenshellCommandOptionValueConverterDictionary"/> class.
        /// </summary>
        /// <param name="logger">The <see cref="ILogger"/> instance.</param>
        public PenshellCommandOptionValueConverterDictionary(ILogger logger)
        {
            _logger = Guard.Argument(logger).NotNull().Value;
        }

        /// <summary>
        /// Adds a <see cref="IPenshellCommandOptionValueConverter"/> instance to this dictionary.
        /// </summary>
        /// <param name="converter">The <see cref="IPenshellCommandOptionValueConverter"/> instance.</param>
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
