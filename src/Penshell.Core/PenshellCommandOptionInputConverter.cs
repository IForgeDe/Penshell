namespace Penshell.Core
{
    using System;
    using CliFx.Services;

    public class PenshellCommandOptionInputConverter : CommandOptionInputConverter
    {
        private readonly PenshellCommandOptionValueConverterRegistry _registry;

        public PenshellCommandOptionInputConverter(PenshellCommandOptionValueConverterRegistry registry)
        {
            _registry = registry ?? throw new ArgumentNullException(nameof(registry));
        }

        protected override object ConvertValue(string value, Type targetType)
        {
            if (_registry.ContainsKey(targetType))
            {
                return _registry[targetType].Convert(value);
            }

            // Default behavior for other types
            return base.ConvertValue(value, targetType);
        }
    }
}
