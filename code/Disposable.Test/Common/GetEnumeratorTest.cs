using System.Collections;

namespace Disposable.Test.Common
{
    internal static class GetEnumeratorTest
    {
        internal static void TestGetEnumerator(this IEnumerable source)
        {
            if (source != null)
            {
                foreach (object o in source) { }
            }
        }
    }
}
