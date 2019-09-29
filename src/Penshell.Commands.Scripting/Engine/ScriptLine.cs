namespace Penshell.Commands.Scripting.Engine
{
    using System.Collections.Generic;

    /// <summary>
    /// The penshell script line class.
    /// </summary>
    public class ScriptLine
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ScriptLine"/> class.
        /// </summary>
        /// <param name="lineNumber">Gets the line number.</param>
        /// <param name="rawLine">Gets the raw line.</param>
        /// <param name="isScriptLine">True if is script line, false if not.</param>
        /// <param name="substitution">Gets the substitution.</param>
        /// <param name="commandArguments">Gets the command arguments.</param>
        internal ScriptLine(long lineNumber, string rawLine, bool isScriptLine, string substitution, string[] commandArguments)
        {
            this.LineNumber = lineNumber;
            this.RawLine = rawLine;
            this.IsScript = isScriptLine;
            this.Substitution = substitution;
            this.CommandArguments = commandArguments;
        }

        /// <summary>
        /// Gets the command arguments.
        /// </summary>
        /// <value>
        /// The command arguments.
        /// </value>
        public IReadOnlyList<string> CommandArguments { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is a script line.
        /// </summary>
        /// <value>
        /// True if this instance is a script line, false if not.
        /// </value>
        public bool IsScript { get; }

        /// <summary>
        /// Gets the line number.
        /// </summary>
        /// <value>
        /// The line number.
        /// </value>
        public long LineNumber { get; }

        /// <summary>
        /// Gets the raw line.
        /// </summary>
        /// <value>
        /// The raw line.
        /// </value>
        public string RawLine { get; }

        /// <summary>
        /// Gets the substitution.
        /// </summary>
        /// <value>
        /// The substitution.
        /// </value>
        public string Substitution { get; }
    }
}
