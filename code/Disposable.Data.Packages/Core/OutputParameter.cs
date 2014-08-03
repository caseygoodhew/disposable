using System;
using System.ComponentModel;
using System.Data;

namespace Disposable.Data.Packages.Core
{
    /// <summary>
    /// Stored method output parameter.
    /// </summary>
    public class OutputParameter : Parameter, IOutputParameter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OutputParameter"/> class.
        /// </summary>
        /// <param name="name">The parameter name.</param>
        /// <param name="dataType">The parameter data type.</param>
        /// <param name="direction">The <see cref="IOutputParameter.Direction"/> of the output parameter.</param>
        public OutputParameter(string name, DataTypes dataType, ParameterDirection direction = ParameterDirection.Output) : base(name, dataType)
        {
            switch (direction)
            {
                case ParameterDirection.Output:
                case ParameterDirection.ReturnValue:
                    break;
                default:
                    throw new InvalidEnumArgumentException("Only Output and ReturnValue are valid ParameterDirections for this call.");
            }

            Direction = direction;
        }

        /// <summary>
        /// The <see cref="IOutputParameter.Direction"/> of the output parameter.
        /// </summary>
        public ParameterDirection Direction { get; private set; }
    }
}
