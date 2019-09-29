namespace Penshell.Commands.Scripting.Engine
{
    using System.IO;

    /// <summary>
    /// This fluent builder class creates an <see cref="IScriptReader"/> instance.
    /// </summary>
    public class ScriptReaderBuilder
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ScriptReaderBuilder"/> class.
        /// </summary>
        /// <param name="scriptFile">The <see cref="FileInfo"/> instance.</param>
        public ScriptReaderBuilder(FileInfo scriptFile)
        {
            this.ScriptFile = scriptFile;
        }

        /// <summary>
        /// Gets the script file.
        /// </summary>
        /// <value>
        /// The script file.
        /// </value>
        public FileInfo ScriptFile { get; }

        /// <summary>
        /// Builds an <see cref="IScriptReader"/> instance.
        /// </summary>
        /// <returns>
        /// An <see cref="IScriptReader"/> instance.
        /// </returns>
        public IScriptReader Build()
        {
            return new ScriptReader(this.ScriptFile);
        }
    }
}
