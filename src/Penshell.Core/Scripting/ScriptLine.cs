namespace Penshell.Core.Scripting
{
    using System.Collections.Generic;

    public class ScriptLine
    {
        internal ScriptLine(int lineNumber, string content, string substitution, string[] commandArguments)
        {
            this.LineNumber = lineNumber;
            this.Content = content;
            this.Substitution = substitution;
            this.CommandArguments = commandArguments;
        }

        public IReadOnlyList<string> CommandArguments { get; }

        public string Content { get; }

        public int LineNumber { get; }

        public string Substitution { get; }
    }
}
