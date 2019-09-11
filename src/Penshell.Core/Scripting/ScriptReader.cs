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

        public string CommandDelimiter { get; } = "=>";

        public FileInfo ScriptFile { get; }

        public IEnumerable<ScriptLine> Read()
        {
            var fileLines = File.ReadAllLines(this.ScriptFile.FullName);
            var scriptLines = new List<ScriptLine>(fileLines.Length);

            var lineNumber = 1;
            foreach (var line in fileLines)
            {
                if (lineNumber == 1)
                {
                    scriptLines.Add(new ScriptLine(lineNumber++, line, string.Empty, line.Split(" ", System.StringSplitOptions.RemoveEmptyEntries)));
                }
                else
                {
                    var lineParts = line.Split(this.CommandDelimiter);
                    scriptLines.Add(new ScriptLine(lineNumber++, line, lineParts[0], lineParts[1].Split(" ", System.StringSplitOptions.RemoveEmptyEntries)));
                }
            }

            return scriptLines;
        }
    }
}
