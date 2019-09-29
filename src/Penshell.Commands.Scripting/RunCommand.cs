namespace Penshell.Commands.Scripting
{
    using System.IO;
    using System.Threading.Tasks;
    using CliFx;
    using CliFx.Attributes;
    using CliFx.Services;
    using Dawn;
    using Penshell.Commands.Scripting.Engine;
    using Penshell.Core;
    using Serilog;

    /// <summary>
    /// Runs a penshell script.
    /// </summary>
    [Command("script run", Description = "Runs a penshell script.")]
    public class RunCommand : ICommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RunCommand"/> class.
        /// </summary>
        /// <param name="registry">
        /// A <see cref="PenshellCommandRegistry"/> instance.
        /// </param>
        /// <param name="factory">
        /// A <see cref="ICommandFactory"/> instance.
        /// </param>
        /// <param name="commandOptionInputConverter">
        /// A <see cref="ICommandOptionInputConverter"/> instance.
        /// </param>
        /// <param name="logger">
        /// A <see cref="ILogger"/> instance.
        /// </param>
        public RunCommand(
            PenshellCommandRegistry registry,
            ICommandFactory factory,
            ICommandOptionInputConverter commandOptionInputConverter,
            ILogger logger)
        {
            this.Registry = registry;
            this.Factory = factory;
            this.CommandOptionInputConverter = commandOptionInputConverter;
            this.Logger = logger;
        }

        /// <summary>
        /// Gets the <see cref="ICommandOptionInputConverter"/> instance.
        /// </summary>
        /// <value>
        /// The <see cref="ICommandOptionInputConverter"/> instance.
        /// </value>
        public ICommandOptionInputConverter CommandOptionInputConverter { get; }

        /// <summary>
        /// Gets the <see cref="ICommandFactory"/> instance.
        /// </summary>
        /// <value>
        /// The <see cref="ICommandFactory"/> instance.
        /// </value>
        public ICommandFactory Factory { get; }

        /// <summary>
        /// Gets the <see cref="ILogger"/> instance.
        /// </summary>
        /// <value>
        /// The <see cref="ILogger"/> instance.
        /// </value>
        public ILogger Logger { get; }

        /// <summary>
        /// Gets or sets the path to the script.
        /// </summary>
        /// <value>
        /// The path to the script.
        /// </value>
        [CommandOption("path", 'p', IsRequired = true, Description = "The path to the script.")]
        public FileInfo? Path { get; set; } = null;

        /// <summary>
        /// Gets the <see cref="PenshellCommandRegistry"/> instance.
        /// </summary>
        /// <value>
        /// The <see cref="PenshellCommandRegistry"/> instance.
        /// </value>
        public PenshellCommandRegistry Registry { get; }

        /// <inheritdoc />
        public Task ExecuteAsync(IConsole console)
        {
            // validate
            console = Guard.Argument(console).NotNull().Value;
            this.Path = Guard.Argument(this.Path).NotNull().Value;

            // perform
            var scriptReader = new ScriptReaderBuilder(this.Path!)
                .Build();
            var scriptPipeline = new ScriptPipelineBuilder()
                .UseScriptReader(scriptReader)
                .UseCommandRegistry(this.Registry)
                .UseCommandFactory(this.Factory)
                .UseCommandOptionInputConverter(this.CommandOptionInputConverter)
                .UseLogger(this.Logger)
                .Build();
            var result = scriptPipeline.Execute();
            console.Output.WriteLine(result);
            return Task.CompletedTask;
        }
    }
}
