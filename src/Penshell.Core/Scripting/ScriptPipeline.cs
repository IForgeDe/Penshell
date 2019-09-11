namespace Penshell.Core.Scripting
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Text;
    using CliFx.Models;
    using CliFx.Services;

    internal class ScriptPipeline : IScriptPipeline
    {
        private readonly ICommandFactory _commandFactory;

        private readonly IConsole _console;

        private readonly PenshellCommandRegistry _registry;

        private readonly IEnumerable<ScriptLine> _scriptLines;

        public ScriptPipeline(
            IConsole console,
            IEnumerable<ScriptLine> scriptLines,
            PenshellCommandRegistry registry,
            ICommandFactory commandFactory)
        {
            _console = console;
            _scriptLines = scriptLines;
            _registry = registry;
            _commandFactory = commandFactory;
        }

        public void Execute()
        {
            var encoding = Encoding.UTF8;
            var commandInputParser = new CommandInputParser();
            var commandSchemaResolver = new CommandSchemaResolver();
            var commandOptionInputConverter = new CommandOptionInputConverter();
            var commandInitializer = new CommandInitializer(commandOptionInputConverter);
            string lastOutputString = string.Empty;
            foreach (var scriptLine in _scriptLines)
            {
                var sciptLineBytes = encoding.GetBytes(scriptLine.Content);
                var scriptLineMemoryStream = new MemoryStream(sciptLineBytes);
                var outputStringBuilder = new StringBuilder();
                using var inputStream = new StreamReader(scriptLineMemoryStream);
                var outputStream = new StringWriter(outputStringBuilder);
                var errorStream = new StringWriter();

                var commandInput = commandInputParser.ParseCommandInput(scriptLine.CommandArguments);

                // perform substitution not in first line
                if (scriptLine.LineNumber != 1)
                {
                    foreach (var option in commandInput.Options)
                    {
                        for (int optionIndex = 0; optionIndex < option.Values.Count; optionIndex++)
                        {
                            var value = option.Values[optionIndex];
                            if (value.Contains(scriptLine.Substitution, StringComparison.InvariantCulture))
                            {
                                option.Values[optionIndex] = value.Replace(scriptLine.Substitution, lastOutputString, StringComparison.InvariantCulture);
                            }
                        }
                    }
                }

                var targetCommandSchema = _registry.CommandSchemas.FindByName(commandInput.CommandName);
                var command = _commandFactory.CreateCommand(targetCommandSchema);
                commandInitializer.InitializeCommand(command, targetCommandSchema, commandInput);
                var virtualConsole = new VirtualConsole(inputStream, outputStream, errorStream);
                command.ExecuteAsync(virtualConsole).Wait();
                lastOutputString = outputStringBuilder.ToString();
                _console.Output.WriteLine($"{scriptLine.LineNumber} [{scriptLine.Content}] : {lastOutputString}");
            }
        }
    }
}
