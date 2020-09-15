namespace Penshell.Commands.Scripting.Engine
{
    using System;
    using System.Text;
    using Dawn;

    /// <summary>
    /// A fluent builder class for creating a <see cref="ScriptLine"/> instance.
    /// </summary>
    public class ScriptLineBuilder
    {
        private string? _commandDelimiter;
        private long _lineNumber;
        private string _rawLine = string.Empty;
        private bool _substitute;

        /// <summary>
        /// Gets the default command delimiter.
        /// </summary>
        /// <value>
        /// The default command delimiter.
        /// </value>
        public string DefaultCommandDelimiter { get; } = "=>";

        /// <summary>
        /// Removes the matching quotes described by parameter.
        /// </summary>
        /// <param name="stringToTrim">
        /// The string to trim.
        /// </param>
        /// <returns>
        /// The resulting string.
        /// </returns>
        public static string RemoveMatchingQuotes(string stringToTrim)
        {
            stringToTrim = Guard.Argument(stringToTrim).NotNull().Value;
            int firstQuoteIndex = stringToTrim.IndexOf('"');
            int lastQuoteIndex = stringToTrim.LastIndexOf('"');
            while (firstQuoteIndex != lastQuoteIndex)
            {
                stringToTrim = stringToTrim.Remove(firstQuoteIndex, 1);
                stringToTrim = stringToTrim.Remove(lastQuoteIndex - 1, 1); // -1 because we've shifted the indicies left by one
                firstQuoteIndex = stringToTrim.IndexOf('"');
                lastQuoteIndex = stringToTrim.LastIndexOf('"');
            }

            return stringToTrim;
        }

        /// <summary>
        /// This method splits an argument string into an string array.
        /// </summary>
        /// <param name="argumentString">
        /// The argument string.
        /// </param>
        /// <returns>
        /// The corresponding string array.
        /// </returns>
        public static string[] SplitCommandLineArgument(string argumentString)
        {
            StringBuilder translatedArguments = new StringBuilder(argumentString).Replace("\\\"", "\r");
            bool insideQuote = false;
            for (int i = 0; i < translatedArguments.Length; i++)
            {
                if (translatedArguments[i] == '"')
                {
                    insideQuote = !insideQuote;
                }

                if (translatedArguments[i] == ' ' && !insideQuote)
                {
                    translatedArguments[i] = '\n';
                }
            }

            string[] toReturn = translatedArguments.ToString().Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < toReturn.Length; i++)
            {
                toReturn[i] = RemoveMatchingQuotes(toReturn[i]);
                toReturn[i] = toReturn[i].Replace("\r", "\"");
            }

            return toReturn;
        }

        /// <summary>
        /// This method builds the scriptline.
        /// </summary>
        /// <returns>
        /// The created <see cref="ScriptLine"/> instance.
        /// </returns>
        public ScriptLine Build()
        {
            _commandDelimiter ??= this.DefaultCommandDelimiter;

            // identify empty lines and comments
            var isScriptLine = true;
            if (_rawLine.Trim().Length < 1)
            {
                isScriptLine = false;
            }
            else if (_rawLine.StartsWith("/", StringComparison.Ordinal) || _rawLine.StartsWith("#", StringComparison.Ordinal))
            {
                isScriptLine = false;
            }

            if (!isScriptLine)
            {
                return new ScriptLine(_lineNumber, _rawLine, isScriptLine, string.Empty, Array.Empty<string>());
            }
            else if (!_substitute)
            {
                var arguments = SplitCommandLineArgument(_rawLine);
                return new ScriptLine(_lineNumber, _rawLine, isScriptLine, string.Empty, arguments);
            }
            else
            {
                var crumbs = _rawLine.Split(new string[] { _commandDelimiter }, StringSplitOptions.RemoveEmptyEntries);
                var arguments = SplitCommandLineArgument(crumbs[1]);
                return new ScriptLine(_lineNumber, _rawLine, isScriptLine, crumbs[0].Trim(), arguments);
            }
        }

        /// <summary>
        /// A fluent method to inject the info, subtitution is desired or not.
        /// </summary>
        /// <param name="value">
        /// The info, subtitution is desired or not.
        /// </param>
        /// <returns>
        /// The fluent instance.
        /// </returns>
        public ScriptLineBuilder DoSubstitute(bool value)
        {
            _substitute = value;
            return this;
        }

        /// <summary>
        /// A fluent method to inject a command delimiter.
        /// </summary>
        /// <param name="commandDelimiter">
        /// The command delimiter.
        /// </param>
        /// <returns>
        /// The fluent instance.
        /// </returns>
        public ScriptLineBuilder UseCommandDelimiter(string commandDelimiter)
        {
            _commandDelimiter = commandDelimiter;
            return this;
        }

        /// <summary>
        /// A fluent method to inject a line number.
        /// </summary>
        /// <param name="lineNumber">
        /// The line number.
        /// </param>
        /// <returns>
        /// The fluent instance.
        /// </returns>
        public ScriptLineBuilder UseLineNumber(long lineNumber)
        {
            _lineNumber = lineNumber;
            return this;
        }

        /// <summary>
        /// A fluent method to inject a raw line.
        /// </summary>
        /// <param name="rawLine">
        /// The raw line.
        /// </param>
        /// <returns>
        /// The fluent instance.
        /// </returns>
        public ScriptLineBuilder UseRawLine(string rawLine)
        {
            _rawLine = rawLine;
            return this;
        }
    }
}
