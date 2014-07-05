using System.Collections.Generic;
using System.Linq;

namespace Disposable.Data.Packages.Core
{
    /// <summary>
    /// Abstract class defining the required attributes to call a stored procedure.
    /// </summary>
    internal abstract class StoredProcedure : StoredMethod, IStoredProcedure
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StoredProcedure"/> class.
        /// </summary>
        /// <param name="package">The <see cref="IPackage"/> that contains the procedure.</param>
        /// <param name="name">The name of the procedure.</param>
        /// <param name="parameters">The list of <see cref="IInputParameter"/>s and <see cref="IOutputParameter"/>s in the package declaration.</param>
        protected StoredProcedure(IPackage package, string name, params IParameter[] parameters)
            : base(package, 
                   name, 
                   parameters.Where(x => x is IInputParameter)
                             .Cast<IInputParameter>()
                             .ToArray())
        {
            OutputParameters = parameters.Where(x => x is IOutputParameter)
                             .Cast<IOutputParameter>()
                             .ToList();
        }

        /// <summary>
        /// The list of <see cref="IOutputParameter"/>s of the procedure.
        /// </summary>
        internal IList<IOutputParameter> OutputParameters { get; private set; }

        /// <summary>
        /// Gets the output parameter values that will be used to call the stored procedure.
        /// </summary>
        /// <returns>A list of the output parameters.</returns>
        public IList<OutputParameterValue> GetOutputParameters()
        {
            return OutputParameters.Select(x => new OutputParameterValue(x)).ToList();
        }
    }
}
