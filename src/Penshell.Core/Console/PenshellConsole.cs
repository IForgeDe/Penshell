namespace Penshell.Core.Console
{
    using System;
    using System.CommandLine.IO;
    using System.Globalization;
    using System.Text;

    /// <summary>
    /// Specialized console class that wraps Penshell functionality.
    /// </summary>
    public class PenshellConsole : SystemConsole, IPenshellConsole
    {
        private StringBuilder? _redirectOutputStringBuilder;

        /// <summary>
        /// Initializes a new instance of the <see cref="PenshellConsole"/> class.
        /// </summary>
        /// <param name="cultureInfo">The <see cref="CultureInfo"/> instance.</param>
        /// <param name="encoding">The <see cref="Encoding"/> instance.</param>
        public PenshellConsole(CultureInfo cultureInfo, Encoding encoding)
            : base()
        {
            this.CultureInfo = cultureInfo ?? throw new ArgumentNullException(nameof(cultureInfo));
            this.Encoding = encoding ?? throw new ArgumentNullException(nameof(encoding));
        }

        /// <inheritdoc/>
        public CultureInfo CultureInfo { get; }

        /// <inheritdoc/>
        public Encoding Encoding { get; }

        /// <inheritdoc/>
        public void RedirectOutput(StringBuilder stringBuilder)
        {
            _redirectOutputStringBuilder = stringBuilder;
        }

        /// <inheritdoc/>
        public void Write(string value)
        {
            if (_redirectOutputStringBuilder != null)
            {
                _redirectOutputStringBuilder.Append(value);
            }
            else
            {
                this.Out.Write(value);
            }
        }

        /// <inheritdoc/>
        public void WriteLine(string value)
        {
            if (_redirectOutputStringBuilder != null)
            {
                _redirectOutputStringBuilder.Append(value).Append(Environment.NewLine);
            }
            else
            {
                this.Out.Write(value);
            }
        }
    }
}
