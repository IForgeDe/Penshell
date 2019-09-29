namespace Penshell.Commands.Scripting.Engine
{
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// This class is the default implementation of the <see cref="IScriptReader"/> interface for reading a penshell script file.
    /// </summary>
    public class ScriptReader : IScriptReader
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ScriptReader"/> class.
        /// </summary>
        /// <param name="scriptFile">
        /// The <see cref="FileInfo"/> instance.
        /// </param>
        public ScriptReader(FileInfo scriptFile)
        {
            this.ScriptFile = scriptFile;
        }

        /// <summary>
        /// Gets the <see cref="FileInfo"/> instance.
        /// </summary>
        /// <value>
        /// The <see cref="FileInfo"/> instance.
        /// </value>
        public FileInfo ScriptFile { get; }

        /// <inheritdoc />
        public IReadOnlyList<ScriptLine> Read()
        {
            var fileLines = File.ReadAllLines(this.ScriptFile.FullName);
            var scriptLines = new List<ScriptLine>(fileLines.Length);

            var lineNumber = 1;
            foreach (var fileLine in fileLines)
            {
                var scriptLine = new ScriptLineBuilder()
                    .UseLineNumber(lineNumber)
                    .UseRawLine(fileLine)
                    .DoSubstitute(lineNumber != 1)
                    .Build();
                scriptLines.Add(scriptLine);
                lineNumber++;
            }

            return scriptLines;
        }
    }
}
