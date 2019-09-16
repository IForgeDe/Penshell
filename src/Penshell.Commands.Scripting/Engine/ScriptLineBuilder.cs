namespace Penshell.Commands.Scripting.Engine
{
    using System;
    using System.Text;
    using Dawn;

    public class ScriptLineBuilder
    {
        private string? _commandDelimiter;
        private long _lineNumber;
        private string _rawLine = string.Empty;
        private bool _substitute = false;

        public string DefaultCommandDelimiter { get; } = "=>";

        public static string RemoveMatchingQuotes(string stringToTrim)
        {
            stringToTrim = Guard.Argument(stringToTrim).NotNull().Value;
            int firstQuoteIndex = stringToTrim.IndexOf('"', StringComparison.Ordinal);
            int lastQuoteIndex = stringToTrim.LastIndexOf('"');
            while (firstQuoteIndex != lastQuoteIndex)
            {
                stringToTrim = stringToTrim.Remove(firstQuoteIndex, 1);
                stringToTrim = stringToTrim.Remove(lastQuoteIndex - 1, 1); // -1 because we've shifted the indicies left by one
                firstQuoteIndex = stringToTrim.IndexOf('"', StringComparison.Ordinal);
                lastQuoteIndex = stringToTrim.LastIndexOf('"');
            }

            return stringToTrim;
        }

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
                toReturn[i] = toReturn[i].Replace("\r", "\"", StringComparison.Ordinal);
            }

            return toReturn;
        }

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
                var crumbs = _rawLine.Split(_commandDelimiter);
                var arguments = SplitCommandLineArgument(crumbs[1]);
                return new ScriptLine(_lineNumber, _rawLine, isScriptLine, crumbs[0].Trim(), arguments);
            }
        }

        public ScriptLineBuilder DoSubstitute(bool value)
        {
            _substitute = value;
            return this;
        }

        public ScriptLineBuilder UseCommandDelimiter(string commandDelimiter)
        {
            _commandDelimiter = commandDelimiter;
            return this;
        }

        public ScriptLineBuilder UseLineNumber(long lineNumber)
        {
            _lineNumber = lineNumber;
            return this;
        }

        public ScriptLineBuilder UseRawLine(string rawLine)
        {
            _rawLine = rawLine;
            return this;
        }
    }
}
