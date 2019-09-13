namespace Penshell.Commands.Scripting.Engine
{
    using System.Collections.Generic;
    using System.IO;

    public class ScriptReader : IScriptReader
    {
        public ScriptReader(FileInfo scriptFile)
        {
            this.ScriptFile = scriptFile;
        }

        public FileInfo ScriptFile { get; }

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
