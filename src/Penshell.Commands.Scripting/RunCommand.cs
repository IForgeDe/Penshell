namespace Penshell.Commands.Scripting
{
    using System.CommandLine;
    using System.CommandLine.Invocation;
    using System.IO;
    using Penshell.Commands.Scripting.Engine;
    using Penshell.Core;
    using Penshell.Core.Console;
    using Serilog;

    /// <summary>
    /// Runs a penshell script.
    /// </summary>
    public class RunCommand : PenshellCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RunCommand"/> class.
        /// </summary>
        /// <param name="console">The <see cref="IPenshellConsole"/> instance.</param>
        /// <param name="registry">
        /// A <see cref="PenshellCommandRegistry"/> instance.
        /// </param>
        /// <param name="logger">
        /// A <see cref="ILogger"/> instance.
        /// </param>
        public RunCommand(
            IPenshellConsole console,
            PenshellCommandRegistry registry,
            ILogger logger)
            : base(console, "run", "Runs a penshell script.")
        {
            this.Registry = registry;
            this.Logger = logger;

            this.AddOption(
                new Option(
                    new string[] { "-p", "--path" },
                    "The path to the script.")
                {
                    Argument = new Argument<FileInfo>(),
                    Required = true,
                });
        }

        /// <summary>
        /// Gets the <see cref="ILogger"/> instance.
        /// </summary>
        /// <value>
        /// The <see cref="ILogger"/> instance.
        /// </value>
        public ILogger Logger { get; }

        /// <summary>
        /// Gets the <see cref="PenshellCommandRegistry"/> instance.
        /// </summary>
        /// <value>
        /// The <see cref="PenshellCommandRegistry"/> instance.
        /// </value>
        public PenshellCommandRegistry Registry { get; }

        /// <summary>
        /// Executes this command.
        /// </summary>
        /// <param name="fileInfo">The script file to run.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "Redesign required.")]
        public void Execute(FileInfo fileInfo)
        {
            var scriptReader = new ScriptReaderBuilder(fileInfo)
                .Build();
            var scriptPipeline = new ScriptPipelineBuilder()
                .UseConsole(this.Console)
                .UseScriptReader(scriptReader)
                .UseCommandRegistry(this.Registry)
                .UseLogger(this.Logger)
                .Build();
            var result = scriptPipeline.Execute();
            this.Console.WriteLine(result);
        }

        /// <inheritdoc />
        protected override ICommandHandler CreateCommandHandler()
        {
            return CommandHandler.Create<FileInfo>((path) => this.Execute(path));
        }
    }
}
