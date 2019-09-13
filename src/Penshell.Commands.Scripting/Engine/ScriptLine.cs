namespace Penshell.Commands.Scripting.Engine
{
    using System.Collections.Generic;

    public class ScriptLine
    {
        internal ScriptLine(long lineNumber, string rawLine, string substitution, string[] commandArguments)
        {
            this.LineNumber = lineNumber;
            this.RawLine = rawLine;
            this.Substitution = substitution;
            this.CommandArguments = commandArguments;
        }

        public IReadOnlyList<string> CommandArguments { get; }

        public long LineNumber { get; }

        public string RawLine { get; }

        public string Substitution { get; }
    }
}
