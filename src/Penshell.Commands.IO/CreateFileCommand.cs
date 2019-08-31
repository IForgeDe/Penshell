namespace Penshell.Commands.IO
{
    using System.IO;
    using System.Threading.Tasks;
    using CliFx.Attributes;
    using CliFx.Services;
    using Dawn;
    using Penshell.Core;
    using Serilog;

    [Command("io createfile", Description = "Creates a file.")]
    public class CreateFileCommand : CommandBase
    {
        public CreateFileCommand(ILogger logger)
            : base(logger)
        {
        }

        [CommandOption("path", 'p', IsRequired = true, Description = "The fully qualified name of the new file, or the relative file name.")]
        public string Path { get; set; } = string.Empty;

        public override Task ExecuteAsync(IConsole console)
        {
            Guard.Argument(this.Path, nameof(this.Path)).NotEmpty();

            var fileInfo = new FileInfo(this.Path);
            var fileStream = fileInfo.Create();
            fileStream.Close();
            console.Output.WriteLine("Success");
            return Task.CompletedTask;
        }
    }
}