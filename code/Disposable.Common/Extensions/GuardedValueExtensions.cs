namespace Disposable.Common.Extensions
{
    public static class GuardedValueExtensions
    {
        private static T GuardedValue<T>(this T? value, string argumentName = "value") where T : struct
        {
            Guard.ArgumentNotNull(value, argumentName);
            return value.Value;
        }
    }
}
