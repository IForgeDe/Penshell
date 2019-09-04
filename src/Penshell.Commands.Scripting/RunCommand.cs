namespace Penshell.Commands.Scripting
{
    using System.IO;
    using System.Threading.Tasks;
    using CliFx;
    using CliFx.Attributes;
    using CliFx.Services;
    using Penshell.Core.Extensions;
    using Penshell.Core.Scripting;

    [Command("script run", Description = "Runs a penshell script.")]
    public class RunCommand : ICommand
    {
        [CommandOption("path", 'p', IsRequired = true, Description = "The path to the script.")]
        public FileInfo ScriptFilePath { get; set; } = null!;

        public Task ExecuteAsync(IConsole console)
        {
            this.ScriptFilePath.GuardNotNull(nameof(this.ScriptFilePath));
            var scriptReader = new ScriptReaderBuilder(this.ScriptFilePath)
                .Build();
            console.Output.Write(scriptReader.Read());
            return Task.CompletedTask;
        }
    }
}
