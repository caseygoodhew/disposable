using System;
using System.Collections.Generic;
using System.Data;

using Disposable.Common.ServiceLocator;
using Disposable.Data.Map.Binding;

namespace Disposable.Data.Map.Data
{
    /// <summary>
    /// Mapper object to map to <see cref="DataSet"/>s and <see cref="IDataReader"/>s.
    /// </summary>
    internal class DataSourceReaderMapper : IDataSourceMapper<DataSourceReader>
    {
        private static readonly Lazy<ITypeBindingFactory> TypeBindingFactory = 
            new Lazy<ITypeBindingFactory>(() => Locator.Current.Instance<ITypeBindingFactory>());
        
        public T GetOne<T>(DataSourceReader dataSourceReader) where T : class, new()
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

        public IEnumerable<T> GetMany<T>(DataSourceReader dataSourceReader) where T : class, new()
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

        private static T GetNext<T>(ITypeBinding<T> typeBinding, DataSourceReader dataSourceReader) where T : class, new()
        {
            T obj;
            TryGetNext(typeBinding, dataSourceReader, out obj);
            return obj;
        }

        private static bool TryGetNext<T>(ITypeBinding<T> typeBinding, DataSourceReader dataSourceReader, out T obj) where T : class, new()
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

        private static void Fill<T>(T obj, ITypeBinding<T> typeBinding, DataSourceReader dataSourceReader) where T : class, new()
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
