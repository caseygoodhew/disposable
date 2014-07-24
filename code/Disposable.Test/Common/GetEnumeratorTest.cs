using System.Collections;

namespace Disposable.Test.Common
{
    public static class GetEnumeratorTest
    {
        public static void TestGetEnumerator(this IEnumerable source)
        {
            if (source != null)
            {
                foreach (object o in source) { }
            }
        }
    }
}
