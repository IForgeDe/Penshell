namespace Penshell.Core.Extensions
{
    using System;

    public static class Guards
    {
        public static string GuardNotEmpty(this string s, string? argName = null) =>
            !string.IsNullOrEmpty(s) ? s : throw new ArgumentException("Cannot be empty.", argName);

        public static T GuardNotNull<T>(this T o, string? argName = null)
                    where T : class =>
            o ?? throw new ArgumentNullException(argName);

        public static int GuardNotZero(this int i, string? argName = null) =>
            i != 0 ? i : throw new ArgumentException("Cannot be zero.", argName);
    }
}
