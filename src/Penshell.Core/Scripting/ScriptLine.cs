namespace Penshell.Core.Scripting
{
    public class ScriptLine
    {
        internal ScriptLine(string content)
        {
            this.Content = content;
        }

        public string Content { get; }
    }
}
