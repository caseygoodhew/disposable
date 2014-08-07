using System.Data;

namespace Disposable.Data.Map.DataSource
{
    /// <summary>
    /// <see cref="DataTable"/> to <see cref="DataSourceReader"/> adapter.
    /// </summary>
    internal sealed class DataTableAdapter : DataReaderAdapter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataTableAdapter"/> class.
        /// </summary>
        /// <param name="table">The underlying <see cref="DataTable"/></param>
        internal DataTableAdapter(DataTable table) : base(table.CreateDataReader())
        {
        }
    }
}
