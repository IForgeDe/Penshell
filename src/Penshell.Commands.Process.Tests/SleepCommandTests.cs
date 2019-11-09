namespace Penshell.Commands.Process.Tests
{
    using NUnit.Framework;

    /// <summary>
    /// Test class for <see cref="SleepCommand"/>.
    /// </summary>
    [TestFixture(TestOf = typeof(SleepCommand))]
    public class SleepCommandTests
    {
        /// <summary>
        /// Test for ExecuteAsync with valid values expects valid value.
        /// </summary>
        /// <param name="milliseconds">The inital millisecond argument.</param>
        /// <param name="seconds">The inital seconds argument.</param>
        /// <param name="minutes">The inital minutes argument.</param>
        /// <param name="hours">The inital hours argument.</param>
        /// <param name="expectedMilliseconds">The expected millisecond result.</param>
        [Test]
        [TestCase(0, 0, 0, 0, 0d)]
        [TestCase(1, 0, 0, 0, 1d)]
        [TestCase(1, 1, 0, 0, 1001d)]
        [TestCase(1, 1, 1, 0, 61001d)]
        [TestCase(1, 1, 1, 1, 3661001d)]
        public void CalculateTimeSpanFromArguments_WithValidValues_ExpectsValidValue(
            int milliseconds,
            int seconds,
            int minutes,
            int hours,
            double expectedMilliseconds)
        {
            // Arrange
            var command = new SleepCommand()
            {
                Milliseconds = milliseconds,
                Seconds = seconds,
                Minutes = minutes,
                Hours = hours,
            };

            // Act
            var timeSpan = command.CalculateTimeSpanFromArguments();

            // Assert
            Assert.That(timeSpan.TotalMilliseconds, Is.EqualTo(expectedMilliseconds));
        }
    }
}
