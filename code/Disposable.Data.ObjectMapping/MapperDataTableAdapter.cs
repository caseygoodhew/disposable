using System;
using System.Data;

namespace Disposable.Data.ObjectMapping
{
    /// <summary>
    /// <see cref="DataTable"/> to <see cref="MapperDataReader"/> adapter.
    /// </summary>
    internal sealed class MapperDataTableAdapter : MapperDataReader
    {
        private readonly DataTable dataTable;

        private int index;

        /// <summary>
        /// Initializes a new instance of the <see cref="MapperDataTableAdapter"/> class.
        /// </summary>
        /// <param name="table">The underlying <see cref="DataTable"/></param>
        internal MapperDataTableAdapter(DataTable table)
        {
            dataTable = table;
        }

        /// <summary>
        /// Gets the number of columns in the current row.
        /// </summary>
        public override int FieldCount
        {
            get
            {
                return dataTable.Columns.Count;
            }
        }

        /// <summary>
        /// Gets the name for the field to find.
        /// </summary>
        /// <param name="i">The zero-based column ordinal.</param>
        /// <returns>The name of the field or the empty string (""), if there is no value to return.</returns>
        public override string GetName(int i)
        {
            return dataTable.Columns[i].ColumnName;
        }

        /// <summary>
        /// Gets the data type information for the specified field.
        /// </summary>
        /// <param name="i">The zero-based column ordinal.</param>
        /// <returns>The data type information for the specified field.</returns>
        public override string GetDataTypeName(int i)
        {
            return dataTable.Columns[i].DataType.Name;
        }

        /// <summary>
        /// Gets the <see cref="Type"/> information corresponding to the type of <see cref="Object"/> that would be returned from <see cref="MapperDataReader.GetValue"/>. 
        /// </summary>
        /// <param name="i">The zero-based column ordinal.</param>
        /// <returns>The <see cref="Type"/> information corresponding to the type of <see cref="Object"/> that would be returned from <see cref="MapperDataReader.GetValue"/>. </returns>
        public override Type GetFieldType(int i)
        {
            return dataTable.Columns[i].DataType;
        }

        /// <summary>
        /// Return the value of the specified field.
        /// </summary>
        /// <param name="i">The zero-based column ordinal.</param>
        /// <returns>The <see cref="Object"/> which will contain the field value upon return.</returns>
        public override object GetValue(int i)
        {
            return dataTable.Rows[index][i];
        }

        /// <summary>
        /// Populates an array of objects with the column values of the current record.
        /// </summary>
        /// <param name="values">An array of <see cref="Object"/> to copy the attribute fields into.</param>
        /// <returns>The number of instances of <see cref="Object"/> in the array.</returns>
        public override int GetValues(object[] values)
        {
            var count = Math.Min(values.Length, dataTable.Columns.Count);
            
            for (var i = 0; i < count; i++)
            {
                values[0] = GetValue(i);
            }

            return count;
        }

        /// <summary>
        /// Gets the value of the specified column as a Boolean.
        /// </summary>
        /// <param name="i">The zero-based column ordinal.</param>
        /// <returns>The value of the column.</returns>
        public override bool GetBoolean(int i)
        {
            return Convert.ToBoolean(GetValue(i));
        }

        /// <summary>
        /// Gets the 8-bit unsigned integer value of the specified column.
        /// </summary>
        /// <param name="i">The zero-based column ordinal.</param>
        /// <returns>The value of the column.</returns>
        public override byte GetByte(int i)
        {
            return Convert.ToByte(GetValue(i));
        }

        /// <summary>
        /// Gets the character value of the specified column.
        /// </summary>
        /// <param name="i">The zero-based column ordinal.</param>
        /// <returns>The value of the column.</returns>
        public override char GetChar(int i)
        {
            return Convert.ToChar(GetValue(i));
        }

        /// <summary>
        /// Gets the 16-bit signed integer value of the specified field.
        /// </summary>
        /// <param name="i">The zero-based column ordinal.</param>
        /// <returns>The value of the column.</returns>
        public override short GetInt16(int i)
        {
            return Convert.ToInt16(GetValue(i));
        }

        /// <summary>
        /// Gets the 32-bit signed integer value of the specified field.
        /// </summary>
        /// <param name="i">The zero-based column ordinal.</param>
        /// <returns>The value of the column.</returns>
        public override int GetInt32(int i)
        {
            return Convert.ToInt32(GetValue(i));
        }

        /// <summary>
        /// Gets the 64-bit signed integer value of the specified field.
        /// </summary>
        /// <param name="i">The zero-based column ordinal.</param>
        /// <returns>The value of the column.</returns>
        public override long GetInt64(int i)
        {
            return Convert.ToInt64(GetValue(i));
        }

        /// <summary>
        /// Gets the single-precision floating point number of the specified field.
        /// </summary>
        /// <param name="i">The zero-based column ordinal.</param>
        /// <returns>The value of the column.</returns>
        public override float GetFloat(int i)
        {
            return Convert.ToSingle(GetValue(i));
        }

        /// <summary>
        /// Gets the double-precision floating point number of the specified field.
        /// </summary>
        /// <param name="i">The zero-based column ordinal.</param>
        /// <returns>The value of the column.</returns>
        public override double GetDouble(int i)
        {
            return Convert.ToDouble(GetValue(i));
        }

        /// <summary>
        /// Gets the string value of the specified field.
        /// </summary>
        /// <param name="i">The zero-based column ordinal.</param>
        /// <returns>The value of the column.</returns>
        public override string GetString(int i)
        {
            return Convert.ToString(GetValue(i));
        }

        /// <summary>
        /// Gets the fixed-position numeric value of the specified field.
        /// </summary>
        /// <param name="i">The zero-based column ordinal.</param>
        /// <returns>The value of the column.</returns>
        public override decimal GetDecimal(int i)
        {
            return Convert.ToDecimal(GetValue(i));
        }

        /// <summary>
        /// Gets the date and time data value of the specified field. 
        /// </summary>
        /// <param name="i">The zero-based column ordinal.</param>
        /// <returns>The value of the column.</returns>
        public override DateTime GetDateTime(int i)
        {
            return Convert.ToDateTime(GetValue(i));
        }

        /// <summary>
        /// Return whether the specified field is set to null.
        /// </summary>
        /// <param name="i">The zero-based column ordinal.</param>
        /// <returns>true if the specified field is set to null; otherwise, false.</returns>
        public override bool IsDBNull(int i)
        {
            return dataTable.Rows[index].IsNull(i);
        }

        /// <summary>
        /// Returns a <see cref="DataTable"/> that describes the column metadata.
        /// </summary>
        /// <returns>A <see cref="DataTable"/> that describes the column metadata.</returns>
        public override DataTable GetSchemaTable()
        {
            return dataTable;
        }

        /// <summary>
        /// Advances the IDataReader to the next record.
        /// </summary>
        /// <returns>true if there are more rows; otherwise, false.</returns>
        internal override bool InternalRead()
        {
            index++;
            return index >= dataTable.Rows.Count;
        }
    }
}
