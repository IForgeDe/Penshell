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
        [TestCase(1d, 1d, 2d)]
        [TestCase(0.1d, 0.1d, 0.2d)]
        public async Task ExecuteAsync_WithValidValues_ExpectsValidValue(double x, double y, double result)
        {
            // Arrange
            using var stdout = new StringWriter();
            var console = new VirtualConsole(stdout);
            var command = new AddCommand(SilentLogger.Instance)
            {
                FirstSummand = x,
                SecondSummand = y,
            };

            // Act
            await command.ExecuteAsync(console).ConfigureAwait(false);

            // Assert
            Assert.That(Convert.ToDouble(stdout.ToString(), CultureInfo.CurrentCulture), Is.EqualTo(result));
        }
    }
}