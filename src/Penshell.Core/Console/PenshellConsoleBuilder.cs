namespace Penshell.Core.Console
{
    using System.CommandLine;
    using System.Globalization;
    using System.Text;

    /// <summary>
    /// A fluent builder for building the penshell console.
    /// </summary>
    public class PenshellConsoleBuilder
    {
        private CultureInfo _cultureInfo = CultureInfo.InvariantCulture;
        private Encoding _encoding = Encoding.UTF8;

        /// <summary>
        /// Builds the <see cref="IConsole"/> instance.
        /// </summary>
        /// <returns>The <see cref="IConsole"/> instance.</returns>
        public IPenshellConsole Build()
        {
            return new PenshellConsole(_cultureInfo, _encoding);
        }

        /// <summary>
        /// Order this builder to use the spcified culture info.
        /// </summary>
        /// <param name="cultureInfo">The <see cref="CultureInfo"/> instance.</param>
        /// <returns>The builder instance.</returns>
        public PenshellConsoleBuilder UseCultureInfo(CultureInfo cultureInfo)
        {
            _cultureInfo = cultureInfo;
            return this;
        }

        /// <summary>
        /// Order this builder to use the spcified encoding.
        /// </summary>
        /// <param name="encoding">The <see cref="Encoding"/> instance.</param>
        /// <returns>The builder instance.</returns>
        public PenshellConsoleBuilder UseEncoding(Encoding encoding)
        {
            _encoding = encoding;
            return this;
        }
    }
}
