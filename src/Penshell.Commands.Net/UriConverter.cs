namespace Penshell.Commands.Net
{
    using System;
    using Penshell.Core;

    public class UriConverter : IPenshellCommandOptionValueConverter
    {
        /// <inheritdoc />
        public Type TargetType => typeof(Uri);

        /// <inheritdoc />
        public object Convert(string value)
        {
            return new Uri(value.Replace("\"", string.Empty, StringComparison.Ordinal));
        }
    }
}
