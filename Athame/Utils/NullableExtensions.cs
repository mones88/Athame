namespace Athame.Utils
{
    public static class NullableExtensions
    {
        public static bool ValueOrFalse(this bool? obj)
        {
            return obj != null && obj.Value;
        }
    }
}
