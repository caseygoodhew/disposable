using Disposable.Common.ServiceLocator;
using Disposable.Data.Map.Binding;
using System;
using System.Collections.Generic;

namespace Disposable.Data.Map.DataSource
{
    /// <summary>
    /// Maps a <see cref="IDataSourceReader"/> to a generic type.
    /// </summary>
    internal class DataSourceReaderMapper : IDataSourceMapper<IDataSourceReader>
    {
        private static readonly Lazy<ITypeBindingFactory> TypeBindingFactory = 
            new Lazy<ITypeBindingFactory>(() => Locator.Current.Instance<ITypeBindingFactory>());

        /// <summary>
        /// Maps exactly one record from a <see cref="DataSourceReader"/>.
        /// </summary>
        /// <typeparam name="T">The generic type to map to.</typeparam>
        /// <param name="dataSourceReader">The <see cref="IDataSourceReader"/> to use to map the object.</param>
        /// <returns>A single object generated from the mapped <see cref="IDataSourceReader"/>.</returns>
        public T GetOne<T>(IDataSourceReader dataSourceReader) where T : class, new()
        {
            var objectBinding = TypeBindingFactory.Value.Get<T>(dataSourceReader);

            var firstObj = GetNext(objectBinding, dataSourceReader);

            if (firstObj == null)
            {
                throw new MapperException("Expected exactly one record. Got none.");
            }

            var nextObj = GetNext(objectBinding, dataSourceReader);

            if (nextObj != null)
            {
                throw new MapperException("Expected exactly one record. Got more than one.");
            }

            return firstObj;
        }

        /// <summary>
        /// Maps all records from a <see cref="IDataSourceReader"/>.
        /// </summary>
        /// <typeparam name="T">The generic type to map to.</typeparam>
        /// <param name="dataSourceReader">The <see cref="DataSourceReader"/> to use to map the object.</param>
        /// <returns>Multiple object generated from the mapped <see cref="DataSourceReader"/>.</returns>
        public IEnumerable<T> GetMany<T>(IDataSourceReader dataSourceReader) where T : class, new()
        {
            var objectBinding = TypeBindingFactory.Value.Get<T>(dataSourceReader);

            var resultSet = new List<T>();

            T obj;
            while (TryGetNext(objectBinding, dataSourceReader, out obj))
            {
                resultSet.Add(obj);
            }

            return resultSet;
        }

        private static T GetNext<T>(ITypeBinding<T> typeBinding, IDataSourceReader dataSourceReader) where T : class, new()
        {
            T obj;
            TryGetNext(typeBinding, dataSourceReader, out obj);
            return obj;
        }

        private static bool TryGetNext<T>(ITypeBinding<T> typeBinding, IDataSourceReader dataSourceReader, out T obj) where T : class, new()
        {
            if (!dataSourceReader.InternalRead())
            {
                obj = null;
                return false;
            }

            obj = new T();

            Fill(obj, typeBinding, dataSourceReader);

            return true;
        }

        private static void Fill<T>(T obj, ITypeBinding<T> typeBinding, IDataSourceReader dataSourceReader) where T : class, new()
        {
            typeBinding.BeginMapping(obj, dataSourceReader);
            
            foreach (var memberMapper in typeBinding)
            {
                int ordinal;
                if (dataSourceReader.TryGetOrdinal(memberMapper.Name, out ordinal))
                {
                    memberMapper.SetValue(obj, dataSourceReader.GetValue(ordinal));
                }
            }

            typeBinding.EndMapping(obj, dataSourceReader);
        }
    }
}
