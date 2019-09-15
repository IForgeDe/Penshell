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

        /// <inheritdoc />
        protected override object ConvertValue(string value, Type targetType)
        {
            // search for specific value converters
            if (_registry.ContainsKey(targetType))
            {
                return _registry[targetType].Convert(value);
            }

            // fix fallback behaviour for values with minus starting
            if (!string.IsNullOrEmpty(value))
            {
                if (value.StartsWith("\"-", StringComparison.Ordinal) && value.EndsWith("\"", StringComparison.Ordinal))
                {
                    if (targetType == typeof(sbyte)
                        || targetType == typeof(short)
                        || targetType == typeof(int)
                        || targetType == typeof(long)
                        || targetType == typeof(float)
                        || targetType == typeof(double)
                        || targetType == typeof(decimal))
                    {
                        value = value.Trim('"');
                    }
                }
            }

            // Default behavior for other types
            return base.ConvertValue(value, targetType);
        }
    }
}
