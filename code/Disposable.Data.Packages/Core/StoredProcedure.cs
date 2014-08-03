using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Disposable.Data.Packages.Core
{
    /// <summary>
    /// Abstract class defining the required attributes to call a stored procedure.
    /// </summary>
    public abstract class StoredProcedure : StoredMethod
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StoredProcedure"/> class.
        /// </summary>
        /// <param name="package">The <see cref="IPackage"/> that contains the procedure.</param>
        /// <param name="name">The name of the procedure.</param>
        /// <param name="parameters">The list of <see cref="IInputParameter"/>s and <see cref="IOutputParameter"/>s in the package declaration.</param>
        protected StoredProcedure(IPackage package, string name, params IParameter[] parameters)
            : base(package, name,  parameters)
        {
            if (OutputParameters.Any(x => x.Direction != ParameterDirection.Output))
            {
                throw new ArgumentException("All output parameter directions must be ParameterDirection.Output.", "parameters");
            }
        }
    }
}
