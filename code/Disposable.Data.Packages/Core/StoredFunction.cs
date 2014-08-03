using System;
using System.Data;
using System.Linq;

namespace Disposable.Data.Packages.Core
{
    /// <summary>
    /// Abstract class defining the required attributes to call a stored function.
    /// </summary>
    public abstract class StoredFunction : StoredMethod
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StoredFunction"/> class.
        /// </summary>
        /// <param name="package">The <see cref="IPackage"/> that contains the function.</param>
        /// <param name="name">The name of the function.</param>
        /// <param name="parameters">The list of <see cref="IInputParameter"/>s and <see cref="IOutputParameter"/> in the package declaration.</param>
        protected StoredFunction(IPackage package, string name, params IParameter[] parameters) : base(package, name, parameters)
        {
            if (OutputParameters.Count != 1)
            {
                throw new ArgumentException("Exactly on output parameter must be provided.", "parameters");
            }

            if (OutputParameters.Single().Direction != ParameterDirection.ReturnValue)
            {
                throw new ArgumentException("The output parameter direction must be ParameterDirection.ReturnValue.", "parameters");
            }
        }
    }
}
