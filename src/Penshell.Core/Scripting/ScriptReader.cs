namespace Penshell.Core.Scripting
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
                var sriptLine = new ScriptLineBuilder()
                    .UseLineNumber(lineNumber)
                    .UseRawLine(fileLine)
                    .UseSubstitution(lineNumber != 1)
                    .Build();
                scriptLines.Add(sriptLine);
                lineNumber++;
            }

            return scriptLines;
        }
    }
}
