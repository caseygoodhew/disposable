using System;
using System.Data;

namespace Disposable.Data.ObjectMapping
{
    /// <summary>
    /// Provides partial implementation framework of IDataReader to be used for <see cref="ObjectMapping"/> services.
    /// </summary>
    public abstract class MapperDataReader : IDataReader
    {
        /// <summary>
        /// Gets the name for the field to find.
        /// </summary>
        /// <param name="i">The zero-based column ordinal.</param>
        /// <returns>The name of the field or the empty string (""), if there is no value to return.</returns>
        public abstract string GetName(int i);

        /// <summary>
        /// Gets the data type information for the specified field.
        /// </summary>
        /// <param name="i">The zero-based column ordinal.</param>
        /// <returns>The data type information for the specified field.</returns>
        public abstract string GetDataTypeName(int i);

        /// <summary>
        /// Gets the <see cref="Type"/> information corresponding to the type of <see cref="Object"/> that would be returned from <see cref="GetValue"/>. 
        /// </summary>
        /// <param name="i">The zero-based column ordinal.</param>
        /// <returns>The <see cref="Type"/> information corresponding to the type of <see cref="Object"/> that would be returned from <see cref="GetValue"/>. </returns>
        public abstract Type GetFieldType(int i);

        /// <summary>
        /// Return the value of the specified field.
        /// </summary>
        /// <param name="i">The zero-based column ordinal.</param>
        /// <returns>The <see cref="Object"/> which will contain the field value upon return.</returns>
        public abstract object GetValue(int i);

        /// <summary>
        /// Populates an array of objects with the column values of the current record.
        /// </summary>
        /// <param name="values">An array of <see cref="Object"/> to copy the attribute fields into.</param>
        /// <returns>The number of instances of <see cref="Object"/> in the array.</returns>
        public abstract int GetValues(object[] values);

        /// <summary>
        /// Return the index of the named field.
        /// </summary>
        /// <param name="name">The name of the field to find.</param>
        /// <returns>The index of the named field.</returns>
        public abstract int GetOrdinal(string name);

        /// <summary>
        /// Gets the value of the specified column as a Boolean.
        /// </summary>
        /// <param name="i">The zero-based column ordinal.</param>
        /// <returns>The value of the column.</returns>
        public abstract bool GetBoolean(int i);

        /// <summary>
        /// Gets the 8-bit unsigned integer value of the specified column.
        /// </summary>
        /// <param name="i">The zero-based column ordinal.</param>
        /// <returns>The value of the column.</returns>
        public abstract byte GetByte(int i);

        /// <summary>
        /// Gets the character value of the specified column.
        /// </summary>
        /// <param name="i">The zero-based column ordinal.</param>
        /// <returns>The value of the column.</returns>
        public abstract char GetChar(int i);

        /// <summary>
        /// Gets the 16-bit signed integer value of the specified field.
        /// </summary>
        /// <param name="i">The zero-based column ordinal.</param>
        /// <returns>The value of the column.</returns>
        public abstract short GetInt16(int i);

        /// <summary>
        /// Gets the 32-bit signed integer value of the specified field.
        /// </summary>
        /// <param name="i">The zero-based column ordinal.</param>
        /// <returns>The value of the column.</returns>
        public abstract int GetInt32(int i);

        /// <summary>
        /// Gets the 64-bit signed integer value of the specified field.
        /// </summary>
        /// <param name="i">The zero-based column ordinal.</param>
        /// <returns>The value of the column.</returns>
        public abstract long GetInt64(int i);

        /// <summary>
        /// Gets the single-precision floating point number of the specified field.
        /// </summary>
        /// <param name="i">The zero-based column ordinal.</param>
        /// <returns>The value of the column.</returns>
        public abstract float GetFloat(int i);

        /// <summary>
        /// Gets the double-precision floating point number of the specified field.
        /// </summary>
        /// <param name="i">The zero-based column ordinal.</param>
        /// <returns>The value of the column.</returns>
        public abstract double GetDouble(int i);

        /// <summary>
        /// Gets the string value of the specified field.
        /// </summary>
        /// <param name="i">The zero-based column ordinal.</param>
        /// <returns>The value of the column.</returns>
        public abstract string GetString(int i);

        /// <summary>
        /// Gets the fixed-position numeric value of the specified field.
        /// </summary>
        /// <param name="i">The zero-based column ordinal.</param>
        /// <returns>The value of the column.</returns>
        public abstract decimal GetDecimal(int i);

        /// <summary>
        /// Gets the date and time data value of the specified field. 
        /// </summary>
        /// <param name="i">The zero-based column ordinal.</param>
        /// <returns>The value of the column.</returns>
        public abstract DateTime GetDateTime(int i);

        /// <summary>
        /// Return whether the specified field is set to null.
        /// </summary>
        /// <param name="i">The zero-based column ordinal.</param>
        /// <returns>true if the specified field is set to null; otherwise, false.</returns>
        public abstract bool IsDBNull(int i);

        /// <summary>
        /// Gets the number of columns in the current row.
        /// </summary>
        public abstract int FieldCount { get; }

        /// <summary>
        /// Returns a <see cref="DataTable"/> that describes the column metadata.
        /// </summary>
        /// <returns>A <see cref="DataTable"/> that describes the column metadata.</returns>
        public abstract DataTable GetSchemaTable();

        /// <summary>
        /// Advances the IDataReader to the next record.
        /// </summary>
        /// <returns>true if there are more rows; otherwise, false.</returns>
        public abstract bool Read();

        object IDataRecord.this[int i]
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        object IDataRecord.this[string name]
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Dispose is implemented to satisfy interface inheritance and is not used. The underlying DataTable should be disposed independantly.
        /// </summary>
        public void Dispose()
        {
            throw new NotSupportedException("Dispose is implemented to satisfy interface inheritance and is not used. The underlying DataTable should be disposed independantly.");
        }

        /// <summary>
        /// Not implemented
        /// </summary>
        /// <param name="i">Not implemented</param>
        /// <param name="fieldOffset">Not implemented</param>
        /// <param name="buffer">Not implemented</param>
        /// <param name="bufferoffset">Not implemented</param>
        /// <param name="length">Not implemented</param>
        /// <returns>Not implemented</returns>
        public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Not implemented
        /// </summary>
        /// <param name="i">Not implemented</param>
        /// <param name="fieldOffset">Not implemented</param>
        /// <param name="buffer">Not implemented</param>
        /// <param name="bufferoffset">Not implemented</param>
        /// <param name="length">Not implemented</param>
        /// <returns>Not implemented</returns>
        public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Not implemented
        /// </summary>
        /// <param name="i">Not implemented</param>
        /// <returns>Not implemented</returns>
        public Guid GetGuid(int i)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Not implemented
        /// </summary>
        /// <param name="i">Not implemented</param>
        /// <returns>Not implemented</returns>
        public IDataReader GetData(int i)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Not implemented
        /// </summary>
        /// <param name="i">Not implemented</param>
        /// <returns>Not implemented</returns>
        public void Close()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Not implemented
        /// </summary>
        /// <param name="i">Not implemented</param>
        /// <returns>Not implemented</returns>
        public bool NextResult()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Not implemented
        /// </summary>
        public int Depth
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Not implemented
        /// </summary>
        public bool IsClosed
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Not implemented
        /// </summary>
        public int RecordsAffected
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}
