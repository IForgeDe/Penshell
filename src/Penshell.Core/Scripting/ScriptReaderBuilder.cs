﻿namespace Penshell.Core.Scripting
{
    using System.IO;
    using Penshell.Core.Extensions;

    public class ScriptReaderBuilder
    {
        public ScriptReaderBuilder(FileInfo scriptFile)
        {
            this.ScriptFile = scriptFile;
        }

        public FileInfo ScriptFile { get; }

        public IScriptReader Build()
        {
            return new ScriptReader(this.ScriptFile);
        }
    }
}
