namespace Disposable.Common.Extensions
{
    public static class GuardedValueExtensions
    {
        public static T GuardedValue<T>(this T? value, string argumentName = "value") where T : struct
        {
            Guard.ArgumentNotNull(value, argumentName);
            return value.Value;
        }
    }
}
