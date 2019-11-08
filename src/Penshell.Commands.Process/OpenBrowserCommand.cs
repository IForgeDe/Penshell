namespace Penshell.Commands.Process
{
    using System;
    using System.Diagnostics;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Threading.Tasks;
    using CliFx;
    using CliFx.Attributes;
    using CliFx.Services;
    using Dawn;

    /// <summary>
    /// Command for opening the browser of the os with a specified Uri.
    /// </summary>
    [Command("process openbrowser", Description = "Opens the browser of a uri.")]
    public class OpenBrowserCommand : ICommand
    {
        /// <summary>
        /// Gets or sets the Uri, which will be opened in the browser.
        /// </summary>
        /// <value>
        /// The Uri, which will be opened in the browser.
        /// </value>
        [CommandOption("url", 'u', IsRequired = true, Description = "The Uri, which will be opened in the browser.")]
        public Uri? Uri { get; set; }

        /// <inheritdoc />
        public Task ExecuteAsync(IConsole console, CancellationToken cancellationToken)
        {
            console = Guard.Argument(console).NotNull().Value;
            this.Uri = Guard.Argument(this.Uri).NotNull().Value;

            var url = this.Uri!.AbsoluteUri;
            try
            {
                Process.Start(url);
            }
            catch
            {
                // hack because of this: https://github.com/dotnet/corefx/issues/10361
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    url = url.Replace("&", "^&", StringComparison.Ordinal);
                    Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Process.Start("xdg-open", url);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Process.Start("open", url);
                }
                else
                {
                    throw;
                }
            }

            console.Output.WriteLine("Success");
            return Task.CompletedTask;
        }
    }
}
