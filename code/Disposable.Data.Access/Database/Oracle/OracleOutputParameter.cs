using Disposable.Data.Packages.Core;
using Oracle.DataAccess.Client;

namespace Disposable.Data.Access.Database.Oracle
{
    /// <summary>
    /// Oracle output parameter.
    /// </summary>
    internal class OracleOutputParameter : OutputParameterValue
    {
        /// <summary>
        /// The underlying oracle parameter.
        /// </summary>
        internal readonly OracleParameter OracleParameter;

        /// <summary>
        /// Initializes a new instance of the <see cref="OracleOutputParameter"/> class.
        /// </summary>
        /// <param name="oracleParameter">The underlying oracle parameter.</param>
        /// <param name="parameter">The underlying <see cref="IOutputParameter"/>.</param>
        internal OracleOutputParameter(OracleParameter oracleParameter, IOutputParameter parameter) : base(parameter)
        {
            OracleParameter = oracleParameter;
        }
    }
}
