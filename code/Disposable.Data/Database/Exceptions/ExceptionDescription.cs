using System;

namespace Disposable.Data.Database.Exceptions
{
    public class ExceptionDescription : IComparable<ExceptionDescription>, IComparable<string>
    {
        public readonly string Name;

        public ExceptionDescription(string name)
        {
            Name = name;
        }

        public int CompareTo(ExceptionDescription other)
        {
            return String.Compare(Name, other.Name, StringComparison.Ordinal);
        }

        public int CompareTo(string other)
        {
            return String.Compare(Name, other, StringComparison.Ordinal);
        }
    }
}
