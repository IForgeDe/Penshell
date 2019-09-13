namespace Penshell.Core
{
    using CliFx.Services;

    public static class PenshellConsoleBuilder
    {
        public static IConsole Build()
        {
            return new PenshellConsole();
        }
    }
}
