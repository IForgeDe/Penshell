namespace Penshell.Commands.Net
{
    using System;
    using System.CommandLine;
    using System.CommandLine.Invocation;
    using System.Net.Http;
    using Penshell.Core;
    using Penshell.Core.Console;

    /// <summary>
    /// Gets the response of a http get method call.
    /// </summary>
    public class HttpGetCommand : PenshellCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HttpGetCommand"/> class.
        /// </summary>
        /// <param name="console">The <see cref="IPenshellConsole"/> instance.</param>
        public HttpGetCommand(IPenshellConsole console)
            : base(console, "httpget", "Gets the response of a http get method call.")
        {
            this.AddOption(
                new Option(
                    new string[] { "-u", "--uri" },
                    "The uri for the http request.")
                {
                    Argument = new Argument<Uri>(),
                    Required = true,
                });
            this.AddOption(
                new Option(
                    new string[] { "-p", "--property" },
                    "The property of the response for the output (default is StatusCode).")
                {
                    Argument = new Argument<string>(getDefaultValue: () => "StatusCode"),
                    Required = false,
                });
        }

        /// <summary>
        /// Executes this command.
        /// </summary>
        /// <param name="uri">The <see cref="Uri"/> instance.</param>
        /// <param name="property">The property.</param>
        public void Execute(Uri uri, string property)
        {
            using var httpClient = new HttpClient();
            var result = httpClient.GetAsync(uri).Result;
            var output = FromProperty(result, property);
            this.Console.WriteLine(output);
        }

        /// <inheritdoc />
        protected override ICommandHandler CreateCommandHandler()
        {
            return CommandHandler.Create<Uri, string>((uri, property) => this.Execute(uri, property));
        }

        private static string FromProperty(HttpResponseMessage httpResponseMessage, string property)
        {
            return property switch
            {
                "Content" => httpResponseMessage.Content.ReadAsStringAsync().Result,
                "Headers" => httpResponseMessage.Headers.ToString(),
                "StatusCode" => httpResponseMessage.StatusCode.ToString(),
                _ => throw new ArgumentException(message: "Invalid property set.", paramName: nameof(property)),
            };
        }
    }
}
