using System;
using System.Collections.Generic;
using System.Linq;
using Disposable.Data.Common.Exceptions;

namespace Disposable.Data.Packages.Core
{
    internal abstract class StoredProcedure : StoredMethod, IStoredProcedure
    {
        internal IList<IOutputParameter> OutputParameters { get; private set; }

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

        public IList<OutputParameterValue> GetOutputParameters()
        {
            return OutputParameters.Select(x => new OutputParameterValue(x)).ToList();
        }
    }
}
