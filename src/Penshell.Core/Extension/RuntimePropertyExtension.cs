namespace Penshell.Core.Extension
{
    using System.Reflection;
    using Dawn;

    /// <summary>
    /// This extension allows to get a runtime property value of an object.
    /// </summary>
    public static class RuntimePropertyExtension
    {
        /// <summary>
        /// Gets the runtime property value of an object.
        /// </summary>
        /// <param name="o">The object to extend.</param>
        /// <param name="propertyName">The name of the property.</param>
        /// <returns>The value of the property as object.</returns>
        public static object? GetPropertyValue(this object o, string propertyName)
        {
            o = Guard.Argument(o).NotNull().Value;
            return o.GetType().GetRuntimeProperty(propertyName)?.GetValue(o);
        }
    }
}
