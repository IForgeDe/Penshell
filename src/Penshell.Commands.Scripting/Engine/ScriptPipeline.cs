namespace Penshell.Commands.Scripting.Engine
{
    using System.Collections.Generic;
    using System.Text;
    using CliFx.Models;
    using CliFx.Services;
    using Penshell.Core;
    using Penshell.Core.Console;
    using Serilog;

    /// <summary>
    /// The default penshell script pipeline implementation.
    /// </summary>
    internal class ScriptPipeline : IScriptPipeline
    {
        private readonly ICommandFactory _commandFactory;
        private readonly ICommandOptionInputConverter _commandOptionInputConverter;
        private readonly ILogger? _logger = null;
        private readonly PenshellCommandRegistry _registry;
        private readonly IEnumerable<ScriptLine> _scriptLines;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScriptPipeline"/> class.
        /// </summary>
        /// <param name="scriptLines">
        /// The script lines.
        /// </param>
        /// <param name="registry">
        /// The <see cref="PenshellCommandRegistry"/> instance.
        /// </param>
        /// <param name="commandFactory">
        /// The <see cref="ICommandFactory"/> instance.
        /// </param>
        /// <param name="commandOptionInputConverter">
        /// The <see cref="ICommandOptionInputConverter"/> instance.
        /// </param>
        /// <param name="logger">
        /// The <see cref="ILogger"/> instance.
        /// </param>
        public ScriptPipeline(
            IEnumerable<ScriptLine> scriptLines,
            PenshellCommandRegistry registry,
            ICommandFactory commandFactory,
            ICommandOptionInputConverter commandOptionInputConverter,
            ILogger? logger)
        {
            _scriptLines = scriptLines;
            _registry = registry;
            _commandFactory = commandFactory;
            _commandOptionInputConverter = commandOptionInputConverter;
            _logger = logger;
        }

        /// <inheritdoc />
        public string Execute()
        {
            var commandInputParser = new CommandInputParser();
            var commandInitializer = new CommandInitializer(_commandOptionInputConverter, new EnvironmentVariablesParser());
            string lastOutputString = string.Empty;
            foreach (var scriptLine in _scriptLines)
            {
                if (!scriptLine.IsScript)
                {
                    _logger?.Information($"Line {scriptLine.LineNumber} [{scriptLine.RawLine}] : Line skipped");
                    continue;
                }

                var outputStringBuilder = new StringBuilder();
                var commandInput = commandInputParser.ParseCommandInput(scriptLine.CommandArguments);
                var substitutedCommandLineArgs = new RawScriptLineBuilder()
                    .UseScriptLine(scriptLine)
                    .UseCommandInput(commandInput)
                    .UseSubstitution(lastOutputString)
                    .Build();
                var transformedScriptLineArguments = new ScriptLineBuilder()
                    .UseLineNumber(scriptLine.LineNumber)
                    .UseRawLine(string.Join(" ", substitutedCommandLineArgs))
                    .DoSubstitute(false)
                    .Build();

                commandInput = commandInputParser.ParseCommandInput(substitutedCommandLineArgs);

                var targetCommandSchema = _registry.CommandSchemas.FindByName(commandInput.CommandName);
                var command = _commandFactory.CreateCommand(targetCommandSchema);
                commandInitializer.InitializeCommand(command, targetCommandSchema, commandInput);
                var virtualConsole = this.CreateVirtualConsole(scriptLine, outputStringBuilder);
                command.ExecuteAsync(virtualConsole).Wait();
                lastOutputString = outputStringBuilder.ToString().Trim();
                _logger?.Information($"Line {scriptLine.LineNumber} [{scriptLine.RawLine}] : {lastOutputString}");
            }

            return lastOutputString;
        }

        private IConsole CreateVirtualConsole(ScriptLine scriptLine, StringBuilder outputStringBuilder)
        {
            return new VirtualConsoleBuilder()
                .UseInput(scriptLine.RawLine)
                .UseOutput(outputStringBuilder)
                .Build();
        }
    }
}
