namespace Penshell.Core.Extension
{
    using System.Reflection;
    using Dawn;

    public static class RuntimePropertyExtension
    {
        public static object? GetPropertyValue(this object o, string propertyName)
        {
            o = Guard.Argument(o).NotNull().Value;
            return o.GetType().GetRuntimeProperty(propertyName)?.GetValue(o);
        }
    }
}
