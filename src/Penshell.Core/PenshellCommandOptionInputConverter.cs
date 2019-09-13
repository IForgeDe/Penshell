namespace Penshell.Core
{
    using System;
    using CliFx.Services;
    using Dawn;

    public class PenshellCommandOptionInputConverter : CommandOptionInputConverter
    {
        private readonly PenshellCommandOptionValueConverterDictionary _registry;

        public PenshellCommandOptionInputConverter(PenshellCommandOptionValueConverterDictionary registry)
        {
            _registry = Guard.Argument(registry).NotNull().Value;
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
