using System.Collections;

namespace Disposable.Test.Runners
{
    internal static class EnumeratorRunner
    {
        internal static void GetEnumerator(this IEnumerable source)
        {
            if (source != null)
            {
                foreach (object o in source) { }
            }
        }
    }
}
