namespace Tests
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Threading.Tasks;
    using CliFx.Services;
    using NUnit.Framework;
    using Penshell.Commands.Math;
    using Penshell.Core;

    [TestFixture]
    public class AddCommandTests
    {
        [Test]
        public async Task ExecuteAsync_WithValidValues_ExpectsValidValue()
        {
            // Arrange
            using var stdout = new StringWriter();
            var console = new VirtualConsole(stdout);
            var command = new AddCommand(SilentLogger.Instance)
            {
                FirstSummand = 1,
                SecondSummand = 2,
            };

            // Act
            await command.ExecuteAsync(console).ConfigureAwait(false);

            // Assert
            Assert.That(Convert.ToDouble(stdout.ToString(), CultureInfo.InvariantCulture), Is.EqualTo(3));
        }
    }
}