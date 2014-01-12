using System;
using System.Collections.Generic;

namespace Disposable.Packages.Core
{
    internal abstract class StoredProcedureDefinition : IStoredProcedureDefinition
    {
        private IDictionary<string, object> _parameters = null;

        public IPackage Package { get; private set; }
        
        public string Procedure { get; private set; }
        
        public IList<InputParameter> InputParameters { get; private set;  }

        public OutputParameter OutputParameter { get; private set; }

        protected StoredProcedureDefinition(string procedure)
            : this(procedure, (IList<InputParameter>)null, null)
        {
        }

        protected StoredProcedureDefinition(string procedure, OutputParameter outputParameter)
            : this(procedure, (IList<InputParameter>)null, outputParameter)
        {
        }

        protected StoredProcedureDefinition(string procedure, InputParameter inputParameter)
            : this(procedure, new List<InputParameter> { inputParameter }, null)
        {
        }

        protected StoredProcedureDefinition(string procedure, InputParameter inputParameter, OutputParameter outputParameter)
            : this(procedure, new List<InputParameter> { inputParameter }, outputParameter)
        {
        }

        protected StoredProcedureDefinition(string procedure, IList<InputParameter> inputParameter)
            : this( procedure, inputParameter, null)
        {
        }

        protected StoredProcedureDefinition(string procedure, IList<InputParameter> inputParameters, OutputParameter outputParameter)
        {
            Procedure = procedure;
            InputParameters = inputParameters;
            OutputParameter = outputParameter;
        }

        internal void SetPackage(IPackage package)
        {
            if (Package != null)
            {
                throw new InvalidOperationException("package is already set");
            }

            Package = package;
        }

        protected void SetParameters(IDictionary<string, object> parameters)
        {
            _parameters = parameters;
        }

        public IDictionary<string, object> GetParameters()
        {
            var parameters = _parameters;

            _parameters = null;

            return parameters;
        }
    }
}
