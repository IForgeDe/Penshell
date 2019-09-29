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

    /// <summary>
    /// Test class for <see cref="AddCommand"/>.
    /// </summary>
    [TestFixture(TestOf=typeof(AddCommand))]
    public class AddCommandTests
    {
        /// <summary>
        /// Test for ExecuteAsync with valid values expects valid value.
        /// </summary>
        /// <param name="x">Valid value x.</param>
        /// <param name="y">Valid value y.</param>
        /// <param name="result">Expected result.</param>
        /// <returns>Task of the test.</returns>
        [Test]
        [TestCase(1d, 1d, 2d)]
        [TestCase(0.1d, 0.1d, 0.2d)]
        public async Task ExecuteAsync_WithValidValues_ExpectsValidValue(double x, double y, double result)
        {
            // Arrange
            using var stdout = new StringWriter();
            var console = new VirtualConsole(stdout);
            var command = new AddCommand()
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
