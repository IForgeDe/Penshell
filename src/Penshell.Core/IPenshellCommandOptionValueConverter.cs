namespace Penshell.Core
{
    using System;

    /// <summary>
    /// Interface for a command option value vonverter.
    /// </summary>
    public interface IPenshellCommandOptionValueConverter
    {
        /// <summary>
        /// Gets the type of the target property of a command.
        /// </summary>
        Type TargetType { get; }

        /// <summary>
        /// Method to convert the string in the target type.
        /// </summary>
        /// <param name="value">The string value to convert.</param>
        /// <returns>The converted value.</returns>
        object Convert(string value);
    }
}
