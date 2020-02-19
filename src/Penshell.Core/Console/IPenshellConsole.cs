namespace Penshell.Core.Console
{
    using System.CommandLine;
    using System.Globalization;
    using System.Text;

    /// <summary>
    /// Interface for a specialized penshell console.
    /// </summary>
    public interface IPenshellConsole : IConsole
    {
        /// <summary>
        /// Gets the <see cref="CultureInfo"/> instance.
        /// </summary>
        CultureInfo CultureInfo { get; }

        /// <summary>
        /// Gets the <see cref="Encoding"/> instance.
        /// </summary>
        Encoding Encoding { get; }
    }
}
