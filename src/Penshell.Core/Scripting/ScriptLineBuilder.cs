﻿namespace Penshell.Core.Scripting
{
    using System;

    public class ScriptLineBuilder
    {
        private string? _commandDelimiter;
        private long _lineNumber;
        private string _rawLine = string.Empty;
        private bool _substitution = false;

        public string DefaultCommandDelimiter { get; } = "=>";

        public ScriptLine Build()
        {
            _commandDelimiter ??= this.DefaultCommandDelimiter;

            if (!_substitution)
            {
                return new ScriptLine(_lineNumber, _rawLine, string.Empty, _rawLine.Split(" ", StringSplitOptions.RemoveEmptyEntries));
            }
            else
            {
                var crumbs = _rawLine.Split(_commandDelimiter);
                return new ScriptLine(_lineNumber, _rawLine, crumbs[0].Trim(), crumbs[1].Trim().Split(" ", StringSplitOptions.RemoveEmptyEntries));
            }
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

        public ScriptLineBuilder UseSubstitution(bool value)
        {
            _substitution = value;
            return this;
        }
    }
}
