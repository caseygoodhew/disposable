using System.Data;

using Disposable.Common.ServiceLocator;
using Disposable.Data.Map.Binding;
using Disposable.Data.Map.Data;

namespace Disposable.Data.Map
{
    /// <summary>
    /// Responsible for registering all services provided by this project.
    /// </summary>
    public static class Registration
    {
        /// <summary>
        /// Registers all services provided by this project.
        /// </summary>
        /// <param name="registrar">The registrar.</param>
        public static void Register(IRegistrar registrar)
        {
            registrar.Register<ITypeBindingFactory>(() => new TypeBindingFactory());
            
            registrar.Register<IDataSourceMapper<DataSet>>(() => new DataSetMapper());
            registrar.Register<IDataSourceMapper<IDataReader>>(() => new DataReaderMapper());
            registrar.Register<IDataSourceMapper<DataSourceReader>>(() => new DataSourceReaderMapper());
        }
    }
}
