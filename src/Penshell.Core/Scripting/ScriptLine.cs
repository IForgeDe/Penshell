namespace Penshell.Core.Scripting
{
    public class ScriptLine
    {
        internal ScriptLine(int lineNumber, string content)
        {
            this.LineNumber = lineNumber;
            this.Content = content;
        }

        public string Content { get; }

        public int LineNumber { get; }
    }
}
