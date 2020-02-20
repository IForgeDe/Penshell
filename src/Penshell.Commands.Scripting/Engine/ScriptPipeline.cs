namespace Penshell.Commands.Scripting.Engine
{
    using System;
    using System.Collections.Generic;
    using System.CommandLine;
    using System.Linq;
    using System.Text;
    using Penshell.Core;
    using Penshell.Core.Console;
    using Serilog;

    /// <summary>
    /// The default penshell script pipeline implementation.
    /// </summary>
    internal class ScriptPipeline : IScriptPipeline
    {
        private readonly ILogger? _logger = null;
        private readonly PenshellCommandRegistry _registry;
        private readonly IEnumerable<ScriptLine> _scriptLines;
        private readonly IPenshellConsole _console;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScriptPipeline"/> class.
        /// </summary>
        /// <param name="console">The <see cref="IPenshellConsole"/> instance.</param>
        /// <param name="scriptLines">The script lines.</param>
        /// <param name="logger">The <see cref="ILogger"/> instance.</param>
        /// <param name="registry">The <see cref="PenshellCommandRegistry"/> instance.</param>
        public ScriptPipeline(
            IPenshellConsole console,
            IEnumerable<ScriptLine> scriptLines,
            ILogger? logger,
            PenshellCommandRegistry registry)
        {
            _console = console ?? throw new ArgumentNullException(nameof(console));
            _scriptLines = scriptLines;
            _logger = logger;
            _registry = registry;
        }

        /// <inheritdoc />
        public string Execute()
        {
            var outputRedirect = new StringBuilder();
            var lastOutputString = string.Empty;
            _console.RedirectOutput(outputRedirect);
            foreach (var scriptLine in _scriptLines)
            {
                if (!scriptLine.IsScript)
                {
                    _logger?.Information($"Line {scriptLine.LineNumber} [{scriptLine.RawLine}] : Line skipped");
                    continue;
                }

                _registry.RootCommand.Invoke(scriptLine.CommandArguments.ToArray());
                lastOutputString = outputRedirect.ToString().Trim();
                outputRedirect.Clear();
                _logger?.Information($"Line {scriptLine.LineNumber} [{scriptLine.RawLine}] : {lastOutputString}");
            }

            return lastOutputString;
        }
    }
}
