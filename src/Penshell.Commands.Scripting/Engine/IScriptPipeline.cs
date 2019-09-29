namespace Penshell.Commands.Scripting.Engine
{
    /// <summary>
    /// Interface for the penshell script pipeline.
    /// </summary>
    public interface IScriptPipeline
    {
        /// <summary>
        /// Executes the pipeline.
        /// </summary>
        /// <returns>
        /// The result string of the last command in the pipeline.
        /// </returns>
        string Execute();
    }
}
