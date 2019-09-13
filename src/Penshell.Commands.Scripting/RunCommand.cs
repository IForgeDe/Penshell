namespace Penshell.Commands.Scripting
{
    using System.IO;
    using System.Threading.Tasks;
    using CliFx;
    using CliFx.Attributes;
    using CliFx.Services;
    using FluentValidation;
    using Penshell.Commands.Scripting.Engine;
    using Penshell.Core;
    using Serilog;

    [Command("script run", Description = "Runs a penshell script.")]
    public class RunCommand : ICommand
    {
        public RunCommand(
            IValidator<RunCommand> validator,
            PenshellCommandRegistry registry,
            ICommandFactory factory,
            ICommandOptionInputConverter commandOptionInputConverter,
            ILogger logger)
        {
            this.Validator = validator;
            this.Registry = registry;
            this.Factory = factory;
            this.CommandOptionInputConverter = commandOptionInputConverter;
            this.Logger = logger;
        }

        public ICommandOptionInputConverter CommandOptionInputConverter { get; }

        public ICommandFactory Factory { get; }

        public ILogger Logger { get; }

        [CommandOption("path", 'p', IsRequired = true, Description = "The path to the script.")]
        public FileInfo? Path { get; set; } = null;

        public PenshellCommandRegistry Registry { get; }

        public IValidator<RunCommand> Validator { get; }

        public Task ExecuteAsync(IConsole console)
        {
            // validate
            this.Validator.ValidateAndThrow(this);

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
