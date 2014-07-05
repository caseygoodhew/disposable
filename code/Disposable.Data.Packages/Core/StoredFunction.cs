using System;
using System.Linq;
using Disposable.Data.Common.Exceptions;

namespace Disposable.Data.Packages.Core
{
    internal abstract class StoredFunction : StoredMethod, IStoredFunction
    {
        private readonly IOutputParameter _outputParameter;

        protected StoredFunction(IPackage package, string name, params IParameter[] parameters)
            : base(package, 
                   name, 
                   parameters.Where(x => x is IInputParameter)
                             .Cast<IInputParameter>()
                             .ToArray())
        {
            _outputParameter = parameters.Single(x => x is IOutputParameter) as IOutputParameter;
        }

        public OutputParameterValue GetOutputParameter()
        {
            return new OutputParameterValue(_outputParameter);
        }
    }
}
