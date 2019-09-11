namespace Penshell.Commands.Scripting
{
    using System.IO;
    using System.Threading.Tasks;
    using CliFx;
    using CliFx.Attributes;
    using CliFx.Services;
    using Penshell.Core;
    using Penshell.Core.Scripting;

    [Command("script run", Description = "Runs a penshell script.")]
    public class RunCommand : ICommand
    {
        public RunCommand(RunCommandValidator validator, PenshellCommandRegistry registry, ICommandFactory factory)
        {
            this.Validator = validator;
            this.Registry = registry;
            this.Factory = factory;
        }

        public ICommandFactory Factory { get; }

        public PenshellCommandRegistry Registry { get; }

        [CommandOption("path", 'p', IsRequired = true, Description = "The path to the script.")]
        public FileInfo? ScriptFilePath { get; set; } = null;

        public RunCommandValidator Validator { get; }

        public Task ExecuteAsync(IConsole console)
        {
            this.Validator.Validate(this);
            var scriptReader = new ScriptReaderBuilder(this.ScriptFilePath!)
                .Build();
            var scriptPipeline = new ScriptPipelineBuilder()
                .UseConsole(console)
                .UseScriptReader(scriptReader)
                .UseCommandRegistry(this.Registry)
                .UserCommandFactory(this.Factory)
                .Build();
            scriptPipeline.Execute();
            return Task.CompletedTask;
        }
    }
}
