using System.Linq;

namespace Disposable.Data.Packages.Core
{
    /// <summary>
    /// Abstract class defining the required attributes to call a stored function.
    /// </summary>
    internal abstract class StoredFunction : StoredMethod, IStoredFunction
    {
        private readonly IOutputParameter outputParameter;

        /// <summary>
        /// Initializes a new instance of the <see cref="StoredFunction"/> class.
        /// </summary>
        /// <param name="package">The <see cref="IPackage"/> that contains the function.</param>
        /// <param name="name">The name of the function.</param>
        /// <param name="parameters">The list of <see cref="IInputParameter"/>s and <see cref="IOutputParameter"/> in the package declaration.</param>
        protected StoredFunction(IPackage package, string name, params IParameter[] parameters)
            : base(package, name, parameters.Where(x => x is IInputParameter).Cast<IInputParameter>().ToArray())
        {
            this.outputParameter = parameters.Single(x => x is IOutputParameter) as IOutputParameter;
        }

        /// <summary>
        /// Gets the output parameter values that will be used to call the stored procedure.
        /// </summary>
        /// <returns>A list of the output parameters.</returns>
        public OutputParameterValue GetOutputParameterValue()
        {
            return new OutputParameterValue(this.outputParameter);
        }
    }
}
