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

        /// <summary>
        /// Redirects the output to the specified <see cref="StringBuilder"/> instance.
        /// </summary>
        /// <param name="stringBuilder">The <see cref="StringBuilder"/> instance.</param>
        void RedirectOutput(StringBuilder stringBuilder);

        /// <summary>
        /// Writes a value to the console output.
        /// </summary>
        /// <param name="value">The output.</param>
        void Write(string value);

        /// <summary>
        /// Writes a value line to the console output.
        /// </summary>
        /// <param name="value">The output.</param>
        void WriteLine(string value);
    }
}
