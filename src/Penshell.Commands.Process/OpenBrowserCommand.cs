namespace Penshell.Commands.Process
{
    using System;
    using System.CommandLine;
    using System.CommandLine.Invocation;
    using System.Diagnostics;
    using System.Runtime.InteropServices;
    using Dawn;
    using Penshell.Core;
    using Penshell.Core.Console;

    /// <summary>
    /// Command for opening the browser of the os with a specified Uri.
    /// </summary>
    public class OpenBrowserCommand : PenshellCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OpenBrowserCommand"/> class.
        /// </summary>
        /// <param name="console">The <see cref="IPenshellConsole"/> instance.</param>
        public OpenBrowserCommand(IPenshellConsole console)
            : base(console, "openbrowser", "Opens the browser of a uri.")
        {
            this.AddOption(
                new Option(
                    new string[] { "-u", "--uri" },
                    "The Uri, which will be opened in the browser.")
                {
                    Argument = new Argument<Uri>(),
                    Required = true,
                });
        }

        /// <summary>
        /// Executes this command.
        /// </summary>
        /// <param name="uri">The Uri, which will be opened in the browser.</param>
        public void Execute(Uri uri)
        {
            uri = Guard.Argument(uri).NotNull();
            var url = uri.AbsoluteUri;
            try
            {
                System.Diagnostics.Process.Start(url);
            }
            catch
            {
                // hack because of this: https://github.com/dotnet/corefx/issues/10361
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    url = url.Replace("&", "^&");
                    System.Diagnostics.Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    System.Diagnostics.Process.Start("xdg-open", url);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    System.Diagnostics.Process.Start("open", url);
                }
                else
                {
                    throw;
                }
            }

            this.Console.Out.Write("Success");
        }

        /// <inheritdoc />
        protected override ICommandHandler CreateCommandHandler()
        {
            return CommandHandler.Create<Uri>((uri) => this.Execute(uri));
        }
    }
}
