namespace Penshell.Commands.Scripting.Engine
{
    using System.Collections.Generic;
    using Serilog;

    /// <summary>
    /// The default penshell script pipeline implementation.
    /// </summary>
    internal class ScriptPipeline : IScriptPipeline
    {
        private readonly ILogger? _logger = null;
        private readonly IEnumerable<ScriptLine> _scriptLines;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScriptPipeline"/> class.
        /// </summary>
        /// <param name="scriptLines">
        /// The script lines.
        /// </param>
        /// <param name="logger">
        /// The <see cref="ILogger"/> instance.
        /// </param>
        public ScriptPipeline(
            IEnumerable<ScriptLine> scriptLines,
            ILogger? logger)
        {
            _scriptLines = scriptLines;
            _logger = logger;
        }

        /// <inheritdoc />
        public string Execute()
        {
            string lastOutputString = string.Empty;
            foreach (var scriptLine in _scriptLines)
            {
                if (!scriptLine.IsScript)
                {
                    _logger?.Information($"Line {scriptLine.LineNumber} [{scriptLine.RawLine}] : Line skipped");
                    continue;
                }

                _logger?.Information($"Line {scriptLine.LineNumber} [{scriptLine.RawLine}] : {lastOutputString}");
            }

            return lastOutputString;
        }
    }
}
