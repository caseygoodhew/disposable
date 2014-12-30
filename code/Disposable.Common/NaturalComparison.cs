using System;

namespace Disposable.Common
{
    public class NaturalComparison<T> where T : IComparable
    {
        private readonly T first;

        public NaturalComparison(T first)
        {
            this.first = first;
        }

        public bool Is(T compareTo)
        {
            return first.CompareTo(compareTo) == 0;
        }

        public bool IsNot(T compareTo)
        {
            return !Is(compareTo);
        }

        public bool Proceeds(T compareTo)
        {
            return first.CompareTo(compareTo) < 0;
        }

        public bool Follows(T compareTo)
        {
            return first.CompareTo(compareTo) > 0;
        }
    }
}
