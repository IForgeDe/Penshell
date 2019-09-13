namespace Penshell.Commands.Net
{
    using System;
    using Dawn;
    using Penshell.Core;

    public class UriConverter : IPenshellCommandOptionValueConverter
    {
        /// <inheritdoc />
        public Type TargetType => typeof(Uri);

        /// <inheritdoc />
        public object Convert(string value)
        {
            value = Guard.Argument(value).NotNull().Value;

            return new Uri(value.Trim('"'));
        }
    }
}
