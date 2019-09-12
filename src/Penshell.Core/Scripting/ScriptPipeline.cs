namespace Penshell.Core.Scripting
{
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using CliFx.Models;
    using CliFx.Services;
    using Serilog;

    internal class ScriptPipeline : IScriptPipeline
    {
        private readonly ICommandFactory _commandFactory;
        private readonly Encoding _encoding = Encoding.UTF8;
        private readonly ILogger? _logger = null;
        private readonly PenshellCommandRegistry _registry;
        private readonly IEnumerable<ScriptLine> _scriptLines;

        public ScriptPipeline(
            IEnumerable<ScriptLine> scriptLines,
            PenshellCommandRegistry registry,
            ICommandFactory commandFactory,
            ILogger? logger)
        {
            _scriptLines = scriptLines;
            _registry = registry;
            _commandFactory = commandFactory;
            _logger = logger;
        }

        public string Execute()
        {
            var commandInputParser = new CommandInputParser();
            var commandOptionInputConverter = new CommandOptionInputConverter();
            var commandInitializer = new CommandInitializer(commandOptionInputConverter);
            string lastOutputString = string.Empty;
            foreach (var scriptLine in _scriptLines)
            {
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
                    .UseSubstitution(false)
                    .Build();

                commandInput = commandInputParser.ParseCommandInput(substitutedCommandLineArgs);

                var targetCommandSchema = _registry.CommandSchemas.FindByName(commandInput.CommandName);
                var command = _commandFactory.CreateCommand(targetCommandSchema);
                commandInitializer.InitializeCommand(command, targetCommandSchema, commandInput);
                var virtualConsole = this.CreateVirtualConsole(scriptLine, outputStringBuilder);
                command.ExecuteAsync(virtualConsole).Wait();
                lastOutputString = outputStringBuilder.ToString().Trim();
                _logger?.Fatal($"Line {scriptLine.LineNumber} [{scriptLine.RawLine}] : {lastOutputString}");
            }

            return lastOutputString;
        }

        private IConsole CreateVirtualConsole(ScriptLine scriptLine, StringBuilder outputStringBuilder)
        {
            var sciptLineBytes = _encoding.GetBytes(scriptLine.RawLine);
            var scriptLineMemoryStream = new MemoryStream(sciptLineBytes);
            using var inputStream = new StreamReader(scriptLineMemoryStream);
            var outputStream = new StringWriter(outputStringBuilder);
            var errorStream = new StringWriter();
            return new VirtualConsole(inputStream, outputStream, errorStream);
        }
    }
}
