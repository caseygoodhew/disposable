using System;
using System.Collections.Generic;
using System.Data;

using Disposable.Common.ServiceLocator;

namespace Disposable.Data.ObjectMapping
{
    /// <summary>
    /// Mapper object to map to <see cref="DataSet"/>s and <see cref="IDataReader"/>s.
    /// </summary>
    public class ObjectMapper : IObjectMapper
    {
        /// <summary>
        /// Maps exactly one record from a <see cref="DataSet"/>.
        /// </summary>
        /// <typeparam name="T">The object type to map to.</typeparam>
        /// <param name="dataSet">The <see cref="DataSet"/> to use to map the object.</param>
        /// <returns>A single object of the mapped <see cref="DataSet"/>.</returns>
        public T GetOne<T>(DataSet dataSet) where T : class, new()
        {
            return GetOne<T>(new MapperDataTableAdapter(dataSet.Tables[0]));
        }

        /// <summary>
        /// Maps exactly one record from a <see cref="IDataReader"/>.
        /// </summary>
        /// <typeparam name="T">The object type to map to.</typeparam>
        /// <param name="dataReader">The <see cref="IDataReader"/> to use to map the object.</param>
        /// <returns>A single object of the mapped <see cref="IDataReader"/>.</returns>
        public T GetOne<T>(IDataReader dataReader) where T : class, new()
        {
            return GetOne<T>(new MapperIDataReaderAdapter(dataReader));
        }

        /// <summary>
        /// Maps all available rows in a <see cref="DataSet"/> to the object type.
        /// </summary>
        /// <typeparam name="T">The object type to map to.</typeparam>
        /// <param name="dataSet">The <see cref="DataSet"/> to use to map the objects.</param>
        /// <returns>IEnumerable{T} objects of the mapped <see cref="DataSet"/>.</returns>
        public IEnumerable<T> GetMany<T>(DataSet dataSet) where T : class, new()
        {
            return GetMany<T>(new MapperDataTableAdapter(dataSet.Tables[0]));
        }

        /// <summary>
        /// Maps all available rows in a <see cref="IDataReader"/> to the object type.
        /// </summary>
        /// <typeparam name="T">The object type to map to.</typeparam>
        /// <param name="dataReader">The <see cref="IDataReader"/> to use to map the objects.</param>
        /// <returns>IEnumerable{T} objects of the mapped <see cref="IDataReader"/>.</returns>
        public IEnumerable<T> GetMany<T>(IDataReader dataReader) where T : class, new()
        {
            return GetMany<T>(new MapperIDataReaderAdapter(dataReader)); 
        }

        private static T GetOne<T>(MapperDataReader mapperDataReader) where T : class, new()
        {
            var objectBinding = GetObjectBinding<T>(mapperDataReader);

            var firstObj = GetNext(objectBinding, mapperDataReader);

            if (firstObj == null)
            {
                throw new ObjectMapperException("Expected exactly one record. Got none.");
            }

            var nextObj = GetNext(objectBinding, mapperDataReader);

            if (nextObj != null)
            {
                throw new ObjectMapperException("Expected exactly one record. Got more than one.");
            }

            return firstObj;
        }

        private static IEnumerable<T> GetMany<T>(MapperDataReader mapperDataReader) where T : class, new()
        {
            var objectBinding = GetObjectBinding<T>(mapperDataReader);

            var resultSet = new List<T>();

            T obj;
            while (TryGetNext(objectBinding, mapperDataReader, out obj))
            {
                resultSet.Add(obj);
            }

            return resultSet;
        }

        private static T GetNext<T>(ITypeBinding<T> typeBinding, MapperDataReader mapperDataReader) where T : class, new()
        {
            T obj;
            TryGetNext(typeBinding, mapperDataReader, out obj);
            return obj;
        }

        private static bool TryGetNext<T>(ITypeBinding<T> typeBinding, MapperDataReader mapperDataReader, out T obj) where T : class, new()
        {
            if (!mapperDataReader.InternalRead())
            {
                obj = null;
                return false;
            }

            obj = new T();

            Fill(obj, typeBinding, mapperDataReader);

            return true;
        }

        private static void Fill<T>(T obj, ITypeBinding<T> typeBinding, MapperDataReader mapperDataReader) where T : class, new()
        {
            typeBinding.BeginMapping(obj, mapperDataReader);
            
            foreach (var memberMapper in typeBinding)
            {
                int ordinal;
                if (!mapperDataReader.TryGetOrdinal(memberMapper.MemberName, out ordinal))
                {
                    continue;
                }

                memberMapper.SetValue(obj, mapperDataReader.GetValue(ordinal));
            }

            typeBinding.EndMapping(obj, mapperDataReader);
        }

        private static ITypeBinding<T> GetObjectBinding<T>(MapperDataReader mapperDataReader) where T : class
        {
            ITypeBinding<T> typeBinding;

            if (!Locator.Current.TryGetInstance(out typeBinding))
            {
                typeBinding = CreateObjectBinding<T>();
            }

            return new MapperDataReaderPartialTypeBinding<T>(typeBinding, mapperDataReader);
        }

        private static ITypeBinding<T> CreateObjectBinding<T>() where T : class
        {
            var objectBinding = new TypeBinding<T>();
            var locator = Locator.Current as Locator;

            if (locator != null)
            {
                locator.Register<ITypeBinding<T>>(() => objectBinding);
            }

            return objectBinding;
        }
    }
}
