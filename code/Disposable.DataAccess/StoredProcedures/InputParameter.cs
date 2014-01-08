﻿using System.Data;

namespace Disposable.DataAccess.StoredProcedures
{
    public class InputParameter : Parameter
    {
        internal readonly bool Required;

        public InputParameter(string name, DbType dataType, bool required = true) : base(name, dataType)
        {
            Required = required;
        }
    }
}
