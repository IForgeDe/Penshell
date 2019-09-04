﻿namespace Penshell.Core.Scripting
{
    using System.Collections.Generic;
    using System.IO;

    internal class ScriptReader : IScriptReader
    {
        internal ScriptReader(FileInfo scriptFile)
        {
            this.ScriptFile = scriptFile;
        }

        internal FileInfo ScriptFile { get; }

        public IEnumerable<ScriptLine> Read()
        {
            var fileLines = File.ReadAllLines(this.ScriptFile.FullName);
            var scriptLines = new List<ScriptLine>(fileLines.Length);

            foreach (var line in fileLines)
            {
                scriptLines.Add(new ScriptLine(line));
            }

            return scriptLines;
        }
    }
}
