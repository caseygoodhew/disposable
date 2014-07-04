using System.Collections.Generic;

namespace Disposable.Data.Access.Database
{
    /// <summary>
    /// Interface for implementations that can convert database objects to well known objects.
    /// </summary>
    public interface IDataObjectConverter
    {
        /// <summary>
        /// Converts an object set to a well known data type.
        /// </summary>
        /// <typeparam name="T">The type to map to.</typeparam>
        /// <param name="values">The values to convert.</param>
        /// <returns>The converted value(s).</returns>
        T ConvertTo<T>(IEnumerable<object> values);
    }
}
